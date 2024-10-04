using BookShopApi.Data;
using BookShopApi.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookShopApi.Tests.Tools;

public static class Helpers
{
    public static ServiceProvider GetServiceProvider()
    {
        ServiceCollection servicesCollection = new();

        // Append required services
        servicesCollection.AddDbContext<BookShopDbContext>(options =>
        {
            options.UseInMemoryDatabase($"BookShop-{Guid.NewGuid()}");
        });

        return servicesCollection.BuildServiceProvider();
    }

    public static void SeedBookShopDbContext(BookShopDbContext context)
    {
        context.Genres.AddRange(
            new Genre
            {
                Id = 1,
                Name = "Adventure"
            },
            new Genre
            {
                Id = 2,
                Name = "Mystery"
            },
            new Genre
            {
                Id = 3,
                Name = "Technology"
            },
            new Genre
            {
                Id = 4,
                Name = "Cooking"
            },
            new Genre
            {
                Id = 5,
                Name = "History"
            });

        context.Books.AddRange(
            new Book
            {
                Id = 1,
                Isbn10 = "1234567890",
                Isbn13 = "9781234567897",
                Title = "The Great Adventure",
                Author = "John Doe",
                PublishedAt = new DateTime(2015, 6, 23, 0, 0, 0, DateTimeKind.Utc),
                Description = "An epic adventure of discovery and survival.",
                Pages = 350,
                GenreId = 1
            },
            new Book
            {
                Id = 2,
                Isbn10 = "0987654321",
                Isbn13 = "9780987654321",
                Title = "Mystery of the Lost Island",
                Author = "Jane Smith",
                PublishedAt = new DateTime(2018, 11, 12, 0, 0, 0, DateTimeKind.Utc),
                Description = "A thrilling mystery set on a remote island.",
                Pages = 280,
                GenreId = 2
            },
            new Book
            {
                Id = 3,
                Isbn10 = "1478523690",
                Isbn13 = "9781478523694",
                Title = "Tech Revolution",
                Author = "Alice Johnson",
                PublishedAt = new DateTime(2020, 3, 15, 0, 0, 0, DateTimeKind.Utc),
                Description = "A deep dive into the rise of technology and its impact.",
                Pages = 400,
                GenreId = 3
            },
            new Book
            {
                Id = 4,
                Isbn10 = "1593572486",
                Isbn13 = "9781593572481",
                Title = "Cooking with Passion",
                Author = "Robert Williams",
                PublishedAt = new DateTime(2021, 9, 5, 0, 0, 0, DateTimeKind.Utc),
                Description = "Delicious recipes and cooking tips from a world-class chef.",
                Pages = 220,
                GenreId = 4
            },
            new Book
            {
                Id = 5,
                Isbn10 = "1231231231",
                Isbn13 = "9781231231230",
                Title = "History of the World",
                Author = "Emily Davis",
                PublishedAt = new DateTime(2017, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                Description = "An in-depth look at key moments in world history.",
                Pages = 500,
                GenreId = 5
            });

        context.SaveChanges();
    }
}
