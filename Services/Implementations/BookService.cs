using LibrarySystem.Data;
using LibrarySystem.DTOs;
using LibrarySystem.DTOs.Books;
using LibrarySystem.Exceptions;
using LibrarySystem.Models;
using LibrarySystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _db;
        public BookService(LibraryDbContext db) { _db = db; }

        public async Task<PagedResultDto<BookDto>> GetAllAsync(int page, int pageSize, string? search)
        {
            var query = _db.Books.Where(x => !x.IsDeleted).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.Title.Contains(search) || x.Author.Contains(search));

            var totalItems = await query.CountAsync();
            var items = await query.OrderBy(x => x.Title).Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new BookDto
                {
                    Id = x.Id,
                    Tytul = x.Title,
                    Autor = x.Author,
                    Isbn = x.Isbn,
                    RokWydania = x.PublicationYear,
                    DostepneEgzemplarze = x.AvailableCopies
                }).ToListAsync();

            return new PagedResultDto<BookDto> { Page = page, PageSize = pageSize, TotalItems = totalItems, Items = items };
        }

        public async Task<BookDto> GetByIdAsync(int id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
                ?? throw new NotFoundException($"Nie znaleziono książki o Id = {id}.");
            return Map(book);
        }

        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            if (await _db.Books.AnyAsync(x => x.Isbn == dto.Isbn && !x.IsDeleted))
                throw new BusinessRuleException("Książka o podanym ISBN już istnieje.");

            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Isbn = dto.Isbn,
                PublicationYear = dto.PublicationYear,
                AvailableCopies = dto.AvailableCopies
            };
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return Map(book);
        }

        public async Task<BookDto> UpdateAsync(int id, UpdateBookDto dto)
        {
            var book = await _db.Books.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
                ?? throw new NotFoundException($"Nie znaleziono książki o Id = {id}.");

            if (await _db.Books.AnyAsync(x => x.Id != id && x.Isbn == dto.Isbn && !x.IsDeleted))
                throw new BusinessRuleException("Inna książka z takim ISBN już istnieje.");

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.Isbn = dto.Isbn;
            book.PublicationYear = dto.PublicationYear;
            book.AvailableCopies = dto.AvailableCopies;
            await _db.SaveChangesAsync();
            return Map(book);
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _db.Books.Include(x => x.Loans).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
                ?? throw new NotFoundException($"Nie znaleziono książki o Id = {id}.");

            if (book.Loans.Any(x => !x.IsReturned))
                throw new BusinessRuleException("Nie można usunąć książki, która ma aktywne wypożyczenia.");

            book.IsDeleted = true;
            await _db.SaveChangesAsync();
        }

        private static BookDto Map(Book book) => new()
        {
            Id = book.Id,
            Tytul = book.Title,
            Autor = book.Author,
            Isbn = book.Isbn,
            RokWydania = book.PublicationYear,
            DostepneEgzemplarze = book.AvailableCopies
        };
    }
}
