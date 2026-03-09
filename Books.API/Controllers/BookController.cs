using Books.BLL.Dtos.Book;
using Microsoft.AspNetCore.Mvc;
using Books.API.Extensions;
using Books.BLL.Services;

namespace Books.API.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _bookService.GetAllAsync();
            return this.GetAction(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _bookService.GetByIdAsync(id);
            return this.GetAction(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBookDto dto)
        {
            var response = await _bookService.CreateAsync(dto);
            return this.GetAction(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateBookDto dto)
        {
            var response = await _bookService.UpdateAsync(dto);
            return this.GetAction(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _bookService.DeleteAsync(id);
            return this.GetAction(response);
        }
    }
}
