using Microsoft.EntityFrameworkCore;
using Books.DAL;
using Books.DAL.Entities;
using Books.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuthorRepository _authorRepository;

        public AuthorController(AppDbContext context, AuthorRepository authorRepository)
        {
            _context = context;
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var authors = await _authorRepository.Authors.ToListAsync();

            return Ok(authors);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AuthorEntity entity)
        {
            bool res = await _authorRepository.CreateAsync(entity);
            if (!res)
            {
                return BadRequest("Не вдалося створити автора");
            }

            return Ok($"Автор '{entity.Name}' успішно створений");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] AuthorEntity entity)
        {
            bool res = await _authorRepository.UpdateAsync(entity);
            if (!res)
            {
                return BadRequest("Не вдалося оновити автора");
            }

            return Ok($"Автор '{entity.Name}' успішно оновлений");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            bool res = await _authorRepository.DeleteAsync(id);
            if (!res)
            {
                return BadRequest("Не вдалося видалити автора");
            }

            return Ok($"Автор успішно видалений");
        }
    }
}
