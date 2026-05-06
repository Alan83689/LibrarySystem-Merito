using LibrarySystem.DTOs;
using LibrarySystem.DTOs.Books;
using LibrarySystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    /// <summary>Kontroler do zarządzania książkami.</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService) { _bookService = bookService; }

        /// <summary>Pobiera listę książek z paginacją i opcjonalnym wyszukiwaniem.</summary>
        [HttpGet]
        public async Task<ActionResult<PagedResultDto<BookDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
            => Ok(await _bookService.GetAllAsync(page, pageSize, search));

        /// <summary>Pobiera jedną książkę po identyfikatorze.</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetById(int id) => Ok(await _bookService.GetByIdAsync(id));

        /// <summary>Dodaje nową książkę.</summary>
        [HttpPost]
        public async Task<ActionResult<BookDto>> Create([FromBody] CreateBookDto dto)
        {
            var created = await _bookService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>Aktualizuje dane książki.</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> Update(int id, [FromBody] UpdateBookDto dto) => Ok(await _bookService.UpdateAsync(id, dto));

        /// <summary>Usuwa książkę logicznie (soft delete).</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) { await _bookService.DeleteAsync(id); return NoContent(); }
    }
}
