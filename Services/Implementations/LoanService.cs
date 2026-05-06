using LibrarySystem.Data;
using LibrarySystem.DTOs;
using LibrarySystem.DTOs.Loans;
using LibrarySystem.Exceptions;
using LibrarySystem.Models;
using LibrarySystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services.Implementations
{
    public class LoanService : ILoanService
    {
        private readonly LibraryDbContext _db;
        private const int MaxLoansPerReader = 3;
        public LoanService(LibraryDbContext db) { _db = db; }

        public async Task<PagedResultDto<LoanDto>> GetAllAsync(int page, int pageSize)
        {
            var query = _db.Loans.Include(x => x.Book).Include(x => x.Reader).AsQueryable();
            var totalItems = await query.CountAsync();
            var items = await query.OrderByDescending(x => x.LoanDate).Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new LoanDto
                {
                    Id = x.Id,
                    BookId = x.BookId,
                    TytulKsiazki = x.Book!.Title,
                    ReaderId = x.ReaderId,
                    Czytelnik = x.Reader!.FirstName + " " + x.Reader.LastName,
                    DataWypozyczenia = x.LoanDate,
                    CzyZwrocono = x.IsReturned,
                    DataZwrotu = x.ReturnDate
                }).ToListAsync();

            return new PagedResultDto<LoanDto> { Page = page, PageSize = pageSize, TotalItems = totalItems, Items = items };
        }

        public async Task<LoanDto> GetByIdAsync(int id)
        {
            var loan = await _db.Loans.Include(x => x.Book).Include(x => x.Reader).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NotFoundException($"Nie znaleziono wypożyczenia o Id = {id}.");
            return Map(loan);
        }

        public async Task<LoanDto> CreateAsync(CreateLoanDto dto)
        {
            var book = await _db.Books.FirstOrDefaultAsync(x => x.Id == dto.BookId && !x.IsDeleted)
                ?? throw new NotFoundException("Podana książka nie istnieje.");
            var reader = await _db.Readers.FirstOrDefaultAsync(x => x.Id == dto.ReaderId && !x.IsDeleted)
                ?? throw new NotFoundException("Podany czytelnik nie istnieje.");

            if (book.AvailableCopies <= 0)
                throw new BusinessRuleException("Brak dostępnych egzemplarzy tej książki.");

            var activeLoans = await _db.Loans.CountAsync(x => x.ReaderId == dto.ReaderId && !x.IsReturned);
            if (activeLoans >= MaxLoansPerReader)
                throw new BusinessRuleException("Czytelnik osiągnął limit 3 aktywnych wypożyczeń.");

            var duplicate = await _db.Loans.AnyAsync(x => x.ReaderId == dto.ReaderId && x.BookId == dto.BookId && !x.IsReturned);
            if (duplicate)
                throw new BusinessRuleException("Czytelnik ma już wypożyczony ten tytuł.");

            var loan = new Loan { BookId = dto.BookId, ReaderId = dto.ReaderId, LoanDate = DateTime.Now, IsReturned = false };
            book.AvailableCopies--;
            _db.Loans.Add(loan);
            await _db.SaveChangesAsync();
            loan.Book = book; loan.Reader = reader;
            return Map(loan);
        }

        public async Task<LoanDto> ReturnAsync(int id)
        {
            var loan = await _db.Loans.Include(x => x.Book).Include(x => x.Reader).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NotFoundException($"Nie znaleziono wypożyczenia o Id = {id}.");

            if (loan.IsReturned)
                throw new BusinessRuleException("To wypożyczenie zostało już zwrócone.");

            loan.IsReturned = true;
            loan.ReturnDate = DateTime.Now;
            if (loan.Book is not null) loan.Book.AvailableCopies++;
            await _db.SaveChangesAsync();
            return Map(loan);
        }

        public async Task DeleteAsync(int id)
        {
            var loan = await _db.Loans.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NotFoundException($"Nie znaleziono wypożyczenia o Id = {id}.");

            if (!loan.IsReturned)
                throw new BusinessRuleException("Nie można usunąć aktywnego wypożyczenia. Najpierw je zwróć.");

            _db.Loans.Remove(loan);
            await _db.SaveChangesAsync();
        }

        private static LoanDto Map(Loan loan) => new()
        {
            Id = loan.Id,
            BookId = loan.BookId,
            TytulKsiazki = loan.Book?.Title ?? string.Empty,
            ReaderId = loan.ReaderId,
            Czytelnik = loan.Reader != null ? $"{loan.Reader.FirstName} {loan.Reader.LastName}" : string.Empty,
            DataWypozyczenia = loan.LoanDate,
            CzyZwrocono = loan.IsReturned,
            DataZwrotu = loan.ReturnDate
        };
    }
}
