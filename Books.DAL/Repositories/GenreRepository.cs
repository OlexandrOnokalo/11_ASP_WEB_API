using Books.DAL;
using Books.DAL.Entities;
using Books.DAL.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Books.DAL.Repositories
{
    public class GenreRepository : GenericRepository<GenreEntity>
    {
        public GenreRepository(AppDbContext context)
            : base(context)
        {

        }

        public IQueryable<GenreEntity> Genres => GetAll();

        public async Task<bool> IsExistAsync(string name)
        {
            return await Genres.AnyAsync(g => g.Name.ToLower() == name.ToLower());
        }

        public async Task<GenreEntity?> GetByNameAsync(string name)
        {
            return await Genres.FirstOrDefaultAsync(g => g.Name.ToLower() == name.ToLower());
        }
    }
}