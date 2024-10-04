using BookShopApi.Controllers;
using BookShopApi.Data;
using BookShopApi.Models;
using BookShopApi.Tests.Tools;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace BookShopApi.Tests.Controllers;

public class GenresControllerTests
{
    [Fact]
    public void GetGenres_DbContextIsNotEmpty_ShouldReturnOkObjectResult()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        GenresController controller = new GenresController(context);

        List<Genre> expectedResult = context.Genres.ToList();

        // Act
        ActionResult<IEnumerable<Genre>> actualResult = controller.GetGenres();

        // Assert
        OkObjectResult? actualOkResult = actualResult.Result as OkObjectResult;
        Assert.IsType<OkObjectResult>(actualOkResult);
        Assert.NotNull(actualOkResult);

        List<Genre>? actualValueResult = actualOkResult.Value as List<Genre>;
        Assert.NotNull(actualValueResult);
        Assert.NotEmpty(actualValueResult);
        Assert.Equal(expectedResult, actualValueResult);
    }

    [Fact]
    public void GetGenres_DbContextIsEmpty_ShouldReturnEmptyListOfGenres()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();

        GenresController controller = new GenresController(context);

        // Act
        ActionResult<IEnumerable<Genre>> actualResult = controller.GetGenres();

        // Assert
        OkObjectResult? actualOkResult = actualResult.Result as OkObjectResult;
        Assert.IsType<OkObjectResult>(actualOkResult);
        Assert.NotNull(actualOkResult);

        List<Genre>? actualValueResult = actualOkResult.Value as List<Genre>;
        Assert.NotNull(actualValueResult);
        Assert.Empty(actualValueResult);
    }

    [Fact]
    public void GetGenre_DbContextIsNotEmpty_ShouldReturnOkObjectResult()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        GenresController controller = new GenresController(context);

        // Act
        ActionResult<Genre> actualResult = controller.GetGenre(1);

        // Assert
        OkObjectResult? actualOkResult = actualResult.Result as OkObjectResult;
        Assert.IsType<OkObjectResult>(actualOkResult);
        Assert.NotNull(actualOkResult);

        Genre? actualValueResult = actualOkResult.Value as Genre;
        Assert.NotNull(actualValueResult);
        Assert.Equal(1, actualValueResult.Id);
    }

    [Fact]
    public void GetGenre_DbContextIsEmpty_ShouldReturnNotFountResult()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();

        GenresController controller = new GenresController(context);

        // Act
        ActionResult<Genre> actualResult = controller.GetGenre(1);

        // Assert
        NotFoundResult? actualNotFoundResult = actualResult.Result as NotFoundResult;
        Assert.NotNull(actualNotFoundResult);
        Assert.IsType<NotFoundResult>(actualNotFoundResult);
    }

    [Fact]
    public void PostGenre_DbContextIsNotEmpty_ShouldReturnCreatedAtActionResult()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        Genre genre = new()
        {
            Id = 6,
            Name = "Thriller"
        };

        GenresController controller = new GenresController(context);

        // Act
        ActionResult<Genre> actualResult = controller.PostGenre(genre);

        // Assert
        CreatedAtActionResult? actualCreatedAtActionResult = actualResult.Result as CreatedAtActionResult;
        Assert.IsType<CreatedAtActionResult>(actualCreatedAtActionResult);
        Assert.NotNull(actualCreatedAtActionResult);

        Genre? actualValueResult = context.Genres.FirstOrDefault(x => x.Id == 6);
        Assert.NotNull(actualValueResult);
        Assert.Equal(genre.Id, actualValueResult.Id);
    }

    [Fact]
    public void PostGenre_DbContextIsEmpty_ShouldReturnCreatedAtActionResult()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();

        Genre genre = new()
        {
            Id = 1,
            Name = "Thriller"
        };

        GenresController controller = new GenresController(context);

        // Act
        ActionResult<Genre> actualResult = controller.PostGenre(genre);

        // Assert
        CreatedAtActionResult? actualCreatedAtActionResult = actualResult.Result as CreatedAtActionResult;
        Assert.IsType<CreatedAtActionResult>(actualCreatedAtActionResult);
        Assert.NotNull(actualCreatedAtActionResult);

        Genre? actualValueResult = context.Genres.FirstOrDefault(x => x.Id == 1);
        Assert.NotNull(actualValueResult);
        Assert.Equal(genre.Id, actualValueResult.Id);
    }

    [Fact]
    public void PutGenre_DbContextIsNotEmpty_ShouldReturnNoContentResult()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        Genre genre = new()
        {
            Id = 1,
            Name = "Thriller"
        };

        GenresController controller = new GenresController(context);

        // Act
        IActionResult actualResult = controller.PutGenre(1, genre);

        // Assert
        NoContentResult? actualNoContentResult = actualResult as NoContentResult;
        Assert.IsType<NoContentResult>(actualNoContentResult);
        Assert.NotNull(actualNoContentResult);

        Genre? actualValueResult = context.Genres.FirstOrDefault(x => x.Id == 1);
        Assert.NotNull(actualValueResult);
        Assert.Equal(genre.Id, actualValueResult.Id);
        Assert.Equal(genre.Name, actualValueResult.Name);
    }

    [Fact]
    public void PostGenre_DbContextIsEmpty_ShouldReturnNotFoundResult()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();

        Genre genre = new()
        {
            Id = 1,
            Name = "Thriller"
        };

        GenresController controller = new GenresController(context);

        // Act
        IActionResult actualResult = controller.PutGenre(1, genre);

        // Assert
        NotFoundResult? actualNoContentResult = actualResult as NotFoundResult;
        Assert.IsType<NotFoundResult>(actualNoContentResult);
        Assert.NotNull(actualNoContentResult);
    }

    [Fact]
    public void DeleteGenre_DbContextIsNotEmpty_ShouldReturnNoContentResult()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        GenresController controller = new GenresController(context);

        // Act
        IActionResult actualResult = controller.DeleteGenre(1);

        // Assert
        NoContentResult? actualNoContentResult = actualResult as NoContentResult;
        Assert.IsType<NoContentResult>(actualNoContentResult);
        Assert.NotNull(actualNoContentResult);

        Genre? actualDbGenreResult = context.Genres.FirstOrDefault(x => x.Id == 1);
        Assert.Null(actualDbGenreResult);
    }

    [Fact]
    public void DeleteGenre_DbContextIsNotEmptyButGenreDoesNotExist_ShouldReturnNotFoundResult()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        GenresController controller = new GenresController(context);

        // Act
        IActionResult actualResult = controller.DeleteGenre(7);

        // Assert
        NotFoundResult? actualNotFoundResult = actualResult as NotFoundResult;
        Assert.IsType<NotFoundResult>(actualNotFoundResult);
        Assert.NotNull(actualNotFoundResult);
    }

    [Fact]
    public void DeleteGenre_DbContextIsEmpty_ShouldReturnNotFoundResult()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();

        GenresController controller = new GenresController(context);

        // Act
        IActionResult actualResult = controller.DeleteGenre(1);

        // Assert
        NotFoundResult? actualNotFoundResult = actualResult as NotFoundResult;
        Assert.IsType<NotFoundResult>(actualNotFoundResult);
        Assert.NotNull(actualNotFoundResult);
    }

    [Fact]
    public void DeleteGenres_DbContextIsNotEmpty_ShouldReturnNoContentResult()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        GenresController controller = new GenresController(context);

        // Act
        IActionResult actualResult = controller.DeleteGenres(1, 2);

        // Assert
        NoContentResult? actualNoContentResult = actualResult as NoContentResult;
        Assert.IsType<NoContentResult>(actualNoContentResult);
        Assert.NotNull(actualNoContentResult);

        Genre? actualDbGenreResult = context.Genres.FirstOrDefault(x => x.Id == 1);
        Assert.Null(actualDbGenreResult);
        actualDbGenreResult = context.Genres.FirstOrDefault(x => x.Id == 2);
        Assert.Null(actualDbGenreResult);
    }

    [Fact]
    public void DeleteGenres_DbContextIsNotEmptyButGenreDoesNotExist_ShouldReturnNotFoundResult()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();
        Helpers.SeedBookShopDbContext(context);

        GenresController controller = new GenresController(context);

        // Act
        IActionResult actualResult = controller.DeleteGenres(7, 8);

        // Assert
        NotFoundResult? actualNotFoundResult = actualResult as NotFoundResult;
        Assert.NotNull(actualNotFoundResult);
        Assert.IsType<NotFoundResult>(actualNotFoundResult);
    }

    [Fact]
    public void DeleteGenres_DbContextIsEmpty_ShouldReturnNotFoundResult()
    {
        // Arrange
        using IServiceScope scope = Helpers.GetServiceProvider().CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        BookShopDbContext context = services.GetRequiredService<BookShopDbContext>();

        GenresController controller = new GenresController(context);

        // Act
        IActionResult actualResult = controller.DeleteGenres(1, 2);

        // Assert
        NotFoundResult? actualNotFoundResult = actualResult as NotFoundResult;
        Assert.IsType<NotFoundResult>(actualNotFoundResult);
        Assert.NotNull(actualNotFoundResult);
    }
}
