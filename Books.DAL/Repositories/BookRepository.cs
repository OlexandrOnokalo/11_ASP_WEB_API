using Books.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DAL.Repositories
{
    public class BookRepository : GenericRepository<BookEntity>
    {
        public readonly AppDbContext _context;
        public BookRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<BookEntity> Books => GetAll();


        public async Task<BookEntity?> GetByNameAsync(string title)
        {
            return await Books
                .FirstOrDefaultAsync(a => a.Title.ToLower() == title.ToLower());
        }

        public async Task<BookEntity> GetByYearAsync(int year)
        {
            return await Books
                .FirstOrDefaultAsync(a => a.PublishYear == year);
        }

        public async Task<BookEntity> GetByRatingAsync(float rating)
        {
            return await Books
                .FirstOrDefaultAsync(a => a.Rating == rating);
        }

        public async Task<List<BookEntity>> GetByGenreAsync(GenreEntity entity)
        {
            return await _context.Books
                .AsNoTracking()
                .Where(b => b.Genres.Any(g => g.Id == entity.Id))
                .ToListAsync();
        }

        public async Task<List<BookEntity>> GetByAuthorAsync(AuthorEntity entity)
        {
            return await _context.Books
                .AsNoTracking()
                .Where(b => b.AuthorId == entity.Id)
                .ToListAsync();
        }





    }
}
