using LibrarySystem.DTOs;
using LibrarySystem.DTOs.Loans;
using LibrarySystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    /// <summary>Kontroler do zarządzania wypożyczeniami.</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;
        public LoansController(ILoanService loanService) { _loanService = loanService; }

        /// <summary>Pobiera listę wypożyczeń z paginacją.</summary>
        [HttpGet]
        public async Task<ActionResult<PagedResultDto<LoanDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
            => Ok(await _loanService.GetAllAsync(page, pageSize));

        /// <summary>Pobiera jedno wypożyczenie po identyfikatorze.</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanDto>> GetById(int id) => Ok(await _loanService.GetByIdAsync(id));

        /// <summary>Tworzy nowe wypożyczenie.</summary>
        [HttpPost]
        public async Task<ActionResult<LoanDto>> Create([FromBody] CreateLoanDto dto)
        {
            var created = await _loanService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>Zwraca wypożyczoną książkę.</summary>
        [HttpPut("{id}/return")]
        public async Task<ActionResult<LoanDto>> Return(int id) => Ok(await _loanService.ReturnAsync(id));

        /// <summary>Usuwa wypożyczenie po zwrocie.</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) { await _loanService.DeleteAsync(id); return NoContent(); }
    }
}
