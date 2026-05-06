using LibrarySystem.DTOs;
using LibrarySystem.DTOs.Readers;
using LibrarySystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    /// <summary>Kontroler do zarządzania czytelnikami.</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ReadersController : ControllerBase
    {
        private readonly IReaderService _readerService;
        public ReadersController(IReaderService readerService) { _readerService = readerService; }

        /// <summary>Pobiera listę czytelników z paginacją i opcjonalnym wyszukiwaniem.</summary>
        [HttpGet]
        public async Task<ActionResult<PagedResultDto<ReaderDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
            => Ok(await _readerService.GetAllAsync(page, pageSize, search));

        /// <summary>Pobiera jednego czytelnika po identyfikatorze.</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReaderDto>> GetById(int id) => Ok(await _readerService.GetByIdAsync(id));

        /// <summary>Dodaje nowego czytelnika.</summary>
        [HttpPost]
        public async Task<ActionResult<ReaderDto>> Create([FromBody] CreateReaderDto dto)
        {
            var created = await _readerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>Aktualizuje dane czytelnika.</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ReaderDto>> Update(int id, [FromBody] UpdateReaderDto dto) => Ok(await _readerService.UpdateAsync(id, dto));

        /// <summary>Usuwa czytelnika logicznie (soft delete).</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) { await _readerService.DeleteAsync(id); return NoContent(); }
    }
}
