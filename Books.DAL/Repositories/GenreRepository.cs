using Books.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DAL.Repositories
{
    public class GenreRepository : GenericRepository<GenreEntity>
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
        public IQueryable<GenreEntity> Genres => GetAll();
        public async Task<GenreEntity?> GetByNameAsync(string name)
        {
            return await Genres
                .FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower());
        }



        public async Task<List<BookEntity>> GetBooksAsync(GenreEntity entity)
        {
            return await _context.Genres
                .AsNoTracking()
                .Where(g => g.Id == entity.Id)
                .SelectMany(g => g.Books)
                .ToListAsync();
        }

        public async Task<bool> AddBookAsync(GenreEntity genre, BookEntity book)
        {
            var genreBooks = await GetBooksAsync(genre);

            if (!genreBooks.Contains(book))
            {
                genre.Books.Add(book);
                return (await _context.SaveChangesAsync()) != 0;
            }
            return false;
        }



    }
}
