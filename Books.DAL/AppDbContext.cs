using Microsoft.EntityFrameworkCore;
using Books.DAL.Entities;

namespace Books.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<BookEntity> Books { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<BooksGenreEntity> BooksGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BookEntity>(e =>
            {
                e.HasKey(b => b.Id);

                e.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

                e.Property(b => b.Description)
                .HasColumnType("text");

                e.Property(b => b.Image)
                .HasMaxLength(100);

                e.Property(b => b.Pages)
                .HasDefaultValue(0);

                e.Property(b => b.Rating)
                .HasDefaultValue(0f);
            });

            // Authors
            builder.Entity<AuthorEntity>(e =>
            {
                e.HasKey(a => a.Id);

                e.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(255);

                e.Property(a => a.Image)
                .HasMaxLength(100);
            });

            // Relationships
            builder.Entity<BookEntity>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.SetNull);

            // BooksGenre Many-to-Many configuration
            builder.Entity<BooksGenreEntity>()
                .HasKey(bg => new { bg.BookId, bg.GenreId });

            builder.Entity<BooksGenreEntity>()
                .HasOne(bg => bg.Book)
                .WithMany(b => b.BooksGenres)
                .HasForeignKey(bg => bg.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<BooksGenreEntity>()
                .HasOne(bg => bg.Genre)
                .WithMany(g => g.BooksGenres)
                .HasForeignKey(bg => bg.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
