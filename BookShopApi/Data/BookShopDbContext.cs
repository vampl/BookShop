using BookShopApi.Models;

using Microsoft.EntityFrameworkCore;

namespace BookShopApi.Data;

public class BookShopDbContext : DbContext
{
    public BookShopDbContext(DbContextOptions<BookShopDbContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }

    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(builder =>
        {
            builder.HasKey(book => book.Isbn);
        });

        modelBuilder.Entity<Genre>(builder =>
        {
            builder.HasKey(genre => genre.Id);

            builder.HasMany<Book>(genre => genre.Books)
                .WithOne(book => book.Genre)
                .HasForeignKey(book => book.GenreId);
        });
    }
}
