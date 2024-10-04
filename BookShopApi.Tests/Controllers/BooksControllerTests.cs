using BookShopApi.Controllers;
using BookShopApi.Data;
using BookShopApi.Models;

using Microsoft.Extensions.DependencyInjection;

using BookShopApi.Tests.Tools;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace BookShopApi.Tests.Controllers;

public class BooksControllerTests
{
    [Fact]
    public void GetBooks_DbContextIsNotEmpty_ShouldReturnListOfBooks()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        List<Book> expectedResult = context.Books.ToList();

        BooksController controller = new BooksController(context);

        // Act
        ActionResult<IEnumerable<Book>> actualResult = controller.GetBooks();

        // Assert
        OkObjectResult? actualOkResult = actualResult.Result as OkObjectResult;
        Assert.IsType<OkObjectResult>(actualOkResult);
        Assert.NotNull(actualOkResult);

        List<Book>? actualValueResult = actualOkResult.Value as List<Book>;
        Assert.NotNull(actualValueResult);
        Assert.NotEmpty(actualValueResult);
        Assert.Equal(expectedResult, actualValueResult);
    }

    [Fact]
    public void GetBooks_DbContextIsEmpty_ShouldReturnEmptyListOfBooks()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();

        BooksController controller = new BooksController(context);

        // Act
        ActionResult<IEnumerable<Book>> actualResult = controller.GetBooks();

        // Assert
        OkObjectResult? actualOkResult = actualResult.Result as OkObjectResult;
        Assert.IsType<OkObjectResult>(actualOkResult);
        Assert.NotNull(actualOkResult);

        List<Book>? actualValueResult = actualOkResult.Value as List<Book>;
        Assert.NotNull(actualValueResult);
        Assert.Empty(actualValueResult);
    }

    [Fact]
    public void GetBook_DbContextIsNotEmpty_ShouldReturnBook()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        Book expectedResult = context.Books.First(x => x.Id == 1);

        BooksController controller = new BooksController(context);

        // Act
        ActionResult<Book> actualResult = controller.GetBook(1);

        // Assert
        OkObjectResult? actualOkResult = actualResult.Result as OkObjectResult;
        Assert.IsType<OkObjectResult>(actualOkResult);
        Assert.NotNull(actualOkResult);

        Book? actualValueResult = actualOkResult.Value as Book;
        Assert.NotNull(actualValueResult);
        Assert.Equal(expectedResult, actualValueResult);
    }

    [Fact]
    public void GetBooks_DbContextIsEmpty_ShouldReturnNotFound()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();

        BooksController controller = new BooksController(context);

        // Act
        ActionResult<Book> actualResult = controller.GetBook(1);

        // Assert
        NotFoundResult? actualNotFoundResult = actualResult.Result as NotFoundResult;
        Assert.IsType<NotFoundResult>(actualNotFoundResult);
        Assert.NotNull(actualNotFoundResult);
    }

    [Fact]
    public void GetBooks_DbContextIsNotEmptyAndBookDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        BooksController controller = new BooksController(context);

        // Act
        ActionResult<Book> actualResult = controller.GetBook(11);

        // Assert
        NotFoundResult? actualNotFoundResult = actualResult.Result as NotFoundResult;
        Assert.IsType<NotFoundResult>(actualNotFoundResult);
        Assert.NotNull(actualNotFoundResult);
    }

    [Fact]
    public void PostBook_DbContextIsNotEmpty_ShouldReturnCreated()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        BooksController controller = new BooksController(context);

        Book book = new()
        {
            Id = 0,
            Isbn10 = "9-8765-4321-0",
            Isbn13 = "978-9-8765-4321-4",
            Title = "The Future of AI",
            Author = "Samuel Green",
            PublishedAt = new DateTime(2022, 5, 10, 0, 0, 0, DateTimeKind.Utc),
            Description = "An insightful exploration of artificial intelligence and its future potential.",
            Pages = 320,
            GenreId = 3
        };

        // Act
        ActionResult<Book> actualResult = controller.PostBook(book);

        // Assert
        CreatedAtActionResult? actualCreatedResult = actualResult.Result as CreatedAtActionResult;
        Assert.IsType<CreatedAtActionResult>(actualCreatedResult);
        Assert.NotNull(actualCreatedResult);

        Book? actualBookResult = actualCreatedResult.Value as Book;
        Book actualDbBookResult = context.Books.First(x => x.Id == 6);
        Assert.IsType<Book>(actualBookResult);
        Assert.NotNull(actualBookResult);
        Assert.Equal(actualDbBookResult.Id, actualBookResult.Id);
    }

    [Fact]
    public void PutBook_DbContextIsNotEmpty_ShouldReturnNoContent()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        BooksController controller = new BooksController(context);

        Book book = new()
        {
            Id = 3,
            Isbn10 = "1478523690",
            Isbn13 = "9781478523694",
            Title = "Tech Revolution",
            Author = "Alice Johnson",
            PublishedAt = new DateTime(2020, 3, 15, 0, 0, 0, DateTimeKind.Utc),
            Description = "A deep dive into the rise of technology and its impact on current and future.",
            Pages = 400,
            GenreId = 3
        };

        // Act
        IActionResult actualResult = controller.PutBook(3, book);

        // Assert
        NoContentResult? actualNoContentResult = actualResult as NoContentResult;
        Assert.IsType<NoContentResult>(actualNoContentResult);
        Assert.NotNull(actualNoContentResult);

        Book actualDbBookResult = context.Books.First(x => x.Id == 3);
        Assert.IsType<Book>(actualDbBookResult);
        Assert.NotNull(actualDbBookResult);
        Assert.Equal(book.Description, actualDbBookResult.Description);
    }

    [Fact]
    public void PutBook_DbContextIsEmpty_ShouldReturnNotFound()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();

        BooksController controller = new BooksController(context);

        Book book = new()
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
        };

        // Act
        IActionResult actualResult = controller.PutBook(1, book);

        // Assert
        NotFoundResult? actualNotFoundResult = actualResult as NotFoundResult;
        Assert.IsType<NotFoundResult>(actualNotFoundResult);
        Assert.NotNull(actualNotFoundResult);
    }

    [Fact]
    public void PutBook_DbContextIsNotEmptyAndBookDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        BooksController controller = new BooksController(context);

        Book book = new()
        {
            Id = 7,
            Isbn10 = "1478523690",
            Isbn13 = "9781478523694",
            Title = "Tech Revolution",
            Author = "Alice Johnson",
            PublishedAt = new DateTime(2020, 3, 15, 0, 0, 0, DateTimeKind.Utc),
            Description = "A deep dive into the rise of technology and its impact on current and future.",
            Pages = 400,
            GenreId = 3
        };

        // Act
        IActionResult actualResult = controller.PutBook(7, book);

        // Assert
        NotFoundResult? actualNotFoundResult = actualResult as NotFoundResult;
        Assert.IsType<NotFoundResult>(actualNotFoundResult);
        Assert.NotNull(actualNotFoundResult);
    }

    [Fact]
    public void DeleteBook_DbContextIsNotEmpty_ShouldReturnNoContent()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        BooksController controller = new BooksController(context);

        // Act
        IActionResult actualResult = controller.DeleteBook(3);

        // Assert
        NoContentResult? actualNoContentResult = actualResult as NoContentResult;
        Assert.IsType<NoContentResult>(actualNoContentResult);
        Assert.NotNull(actualNoContentResult);

        Book? actualDbBookResult = context.Books.FirstOrDefault(x => x.Id == 3);
        Assert.Null(actualDbBookResult);
    }

    [Fact]
    public void DeleteBook_DbContextIsEmpty_ShouldReturnNotFound()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();

        BooksController controller = new BooksController(context);

        // Act
        IActionResult actualResult = controller.DeleteBook(3);

        // Assert
        NotFoundResult? actualNotFoundResult = actualResult as NotFoundResult;
        Assert.IsType<NotFoundResult>(actualNotFoundResult);
        Assert.NotNull(actualNotFoundResult);
    }

    [Fact]
    public void DeleteBook_DbContextIsNotEmptyButBookDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        BooksController controller = new BooksController(context);

        // Act
        IActionResult actualResult = controller.DeleteBook(7);

        // Assert
        NotFoundResult? actualNotFoundResult = actualResult as NotFoundResult;
        Assert.IsType<NotFoundResult>(actualNotFoundResult);
        Assert.NotNull(actualNotFoundResult);
    }

    [Fact]
    public void DeleteBooks_DbContextIsNotEmpty_ShouldReturnNoContent()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        BooksController controller = new BooksController(context);

        // Act
        IActionResult actualResult = controller.DeleteBooks(3, 4);

        // Assert
        NoContentResult? actualNoContentResult = actualResult as NoContentResult;
        Assert.IsType<NoContentResult>(actualNoContentResult);
        Assert.NotNull(actualNoContentResult);

        Book? actualDbBookResult = context.Books.FirstOrDefault(x => x.Id == 3);
        Assert.Null(actualDbBookResult);

        actualDbBookResult = context.Books.FirstOrDefault(x => x.Id == 4);
        Assert.Null(actualDbBookResult);
    }

    [Fact]
    public void DeleteBooks_DbContextIsEmpty_ShouldReturnNotFound()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();

        BooksController controller = new BooksController(context);

        // Act
        IActionResult actualResult = controller.DeleteBooks(3, 4);

        // Assert
        NotFoundResult? actualNotFoundResult = actualResult as NotFoundResult;
        Assert.IsType<NotFoundResult>(actualNotFoundResult);
        Assert.NotNull(actualNotFoundResult);
    }

    [Fact]
    public void DeleteBooks_DbContextIsNotEmptyButBookDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        BooksController controller = new BooksController(context);

        // Act
        IActionResult actualResult = controller.DeleteBooks(7, 8);

        // Assert
        NotFoundResult? actualNotFoundResult = actualResult as NotFoundResult;
        Assert.IsType<NotFoundResult>(actualNotFoundResult);
        Assert.NotNull(actualNotFoundResult);
    }

}
