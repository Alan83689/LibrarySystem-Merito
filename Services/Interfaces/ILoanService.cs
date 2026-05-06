using LibrarySystem.DTOs;
using LibrarySystem.DTOs.Loans;
namespace LibrarySystem.Services.Interfaces
{
    public interface ILoanService
    {
        Task<PagedResultDto<LoanDto>> GetAllAsync(int page, int pageSize);
        Task<LoanDto> GetByIdAsync(int id);
        Task<LoanDto> CreateAsync(CreateLoanDto dto);
        Task<LoanDto> ReturnAsync(int id);
        Task DeleteAsync(int id);
    }
}
