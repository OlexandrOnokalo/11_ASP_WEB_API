using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DAL.Entities
{
    public class GenreEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<BooksGenreEntity> BooksGenres { get; set; } = [];
    }
}
