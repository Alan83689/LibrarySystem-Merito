using LibrarySystem.DTOs;
using LibrarySystem.DTOs.Readers;
namespace LibrarySystem.Services.Interfaces
{
    public interface IReaderService
    {
        Task<PagedResultDto<ReaderDto>> GetAllAsync(int page, int pageSize, string? search);
        Task<ReaderDto> GetByIdAsync(int id);
        Task<ReaderDto> CreateAsync(CreateReaderDto dto);
        Task<ReaderDto> UpdateAsync(int id, UpdateReaderDto dto);
        Task DeleteAsync(int id);
    }
}
