using LibrarySystem.Data;
using LibrarySystem.DTOs;
using LibrarySystem.DTOs.Readers;
using LibrarySystem.Exceptions;
using LibrarySystem.Models;
using LibrarySystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services.Implementations
{
    public class ReaderService : IReaderService
    {
        private readonly LibraryDbContext _db;
        public ReaderService(LibraryDbContext db) { _db = db; }

        public async Task<PagedResultDto<ReaderDto>> GetAllAsync(int page, int pageSize, string? search)
        {
            var query = _db.Readers.Where(x => !x.IsDeleted).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search) || x.LibraryCardNumber.Contains(search));

            var totalItems = await query.CountAsync();
            var items = await query.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new ReaderDto
                {
                    Id = x.Id,
                    ImieNazwisko = x.FirstName + " " + x.LastName,
                    NumerKarty = x.LibraryCardNumber,
                    Email = x.Email
                }).ToListAsync();

            return new PagedResultDto<ReaderDto> { Page = page, PageSize = pageSize, TotalItems = totalItems, Items = items };
        }

        public async Task<ReaderDto> GetByIdAsync(int id)
        {
            var reader = await _db.Readers.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
                ?? throw new NotFoundException($"Nie znaleziono czytelnika o Id = {id}.");
            return Map(reader);
        }

        public async Task<ReaderDto> CreateAsync(CreateReaderDto dto)
        {
            if (await _db.Readers.AnyAsync(x => x.LibraryCardNumber == dto.LibraryCardNumber && !x.IsDeleted))
                throw new BusinessRuleException("Czytelnik z takim numerem karty już istnieje.");
            if (await _db.Readers.AnyAsync(x => x.Email == dto.Email && !x.IsDeleted))
                throw new BusinessRuleException("Czytelnik z takim adresem email już istnieje.");

            var reader = new Reader
            {
                FirstName = dto.FirstName, LastName = dto.LastName,
                LibraryCardNumber = dto.LibraryCardNumber, Email = dto.Email
            };
            _db.Readers.Add(reader);
            await _db.SaveChangesAsync();
            return Map(reader);
        }

        public async Task<ReaderDto> UpdateAsync(int id, UpdateReaderDto dto)
        {
            var reader = await _db.Readers.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
                ?? throw new NotFoundException($"Nie znaleziono czytelnika o Id = {id}.");

            if (await _db.Readers.AnyAsync(x => x.Id != id && x.LibraryCardNumber == dto.LibraryCardNumber && !x.IsDeleted))
                throw new BusinessRuleException("Inny czytelnik z takim numerem karty już istnieje.");
            if (await _db.Readers.AnyAsync(x => x.Id != id && x.Email == dto.Email && !x.IsDeleted))
                throw new BusinessRuleException("Inny czytelnik z takim adresem email już istnieje.");

            reader.FirstName = dto.FirstName;
            reader.LastName = dto.LastName;
            reader.LibraryCardNumber = dto.LibraryCardNumber;
            reader.Email = dto.Email;
            await _db.SaveChangesAsync();
            return Map(reader);
        }

        public async Task DeleteAsync(int id)
        {
            var reader = await _db.Readers.Include(x => x.Loans).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
                ?? throw new NotFoundException($"Nie znaleziono czytelnika o Id = {id}.");

            if (reader.Loans.Any(x => !x.IsReturned))
                throw new BusinessRuleException("Nie można usunąć czytelnika, który ma aktywne wypożyczenia.");

            reader.IsDeleted = true;
            await _db.SaveChangesAsync();
        }

        private static ReaderDto Map(Reader reader) => new()
        {
            Id = reader.Id,
            ImieNazwisko = $"{reader.FirstName} {reader.LastName}",
            NumerKarty = reader.LibraryCardNumber,
            Email = reader.Email
        };
    }
}
