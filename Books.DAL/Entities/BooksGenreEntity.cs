using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DAL.Entities
{
    public class BooksGenreEntity
    {
        public int BookId { get; set; }
        public BookEntity Book { get; set; } = null!;

        public int GenreId { get; set; }
        public GenreEntity Genre { get; set; } = null!;
    }
}
