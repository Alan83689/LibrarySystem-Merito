using LibrarySystem.DTOs;
using LibrarySystem.DTOs.Books;
namespace LibrarySystem.Services.Interfaces
{
    public interface IBookService
    {
        Task<PagedResultDto<BookDto>> GetAllAsync(int page, int pageSize, string? search);
        Task<BookDto> GetByIdAsync(int id);
        Task<BookDto> CreateAsync(CreateBookDto dto);
        Task<BookDto> UpdateAsync(int id, UpdateBookDto dto);
        Task DeleteAsync(int id);
    }
}
