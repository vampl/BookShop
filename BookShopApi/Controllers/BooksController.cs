using BookShopApi.Data;

using Microsoft.AspNetCore.Mvc;

using BookShopApi.Models;

using Microsoft.EntityFrameworkCore;

namespace BookShopApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookShopDbContext _context;

    public BooksController(BookShopDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetBooks()
    {
        return Ok(_context.Books.ToList());
    }

    [HttpGet("{isbn:long}")]
    public ActionResult<Book> GetBook(long isbn)
    {
        Book? book = _context.Books.Find(isbn);
        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public ActionResult<Book> PostBook([FromBody] Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();

        return CreatedAtAction("PostBook", book);
    }

    [HttpPut("{isbn:long}")]
    public IActionResult PutBook(long isbn, [FromBody] Book modifiedBook)
    {
        Book? book = _context.Books.Find(isbn);
        if (book is null)
        {
            return NotFound();
        }

        _context.Entry(book).CurrentValues.SetValues(modifiedBook);
        _context.Entry(book).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{isbn:long}")]
    public IActionResult DeleteBook(long isbn)
    {
        Book? book = _context.Books.Find(isbn);
        if (book is null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        _context.SaveChanges();

        return NoContent();
    }
}
