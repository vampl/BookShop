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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Book>> GetBooks()
    {
        return Ok(_context.Books.ToList());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Book> GetBook(int id)
    {
        Book? book = _context.Books.Find(id);
        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<Book> PostBook([FromBody] Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();

        return CreatedAtAction("PostBook", book);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult PutBook(int id, [FromBody] Book modifiedBook)
    {
        Book? book = _context.Books.Find(id);
        if (book is null)
        {
            return NotFound();
        }

        _context.Entry(book).CurrentValues.SetValues(modifiedBook);
        _context.Entry(book).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteBook(int id)
    {
        Book? book = _context.Books.Find(id);
        if (book is null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteBooks([FromQuery] params int[] ids)
    {
        List<Book> books = new();
        foreach (int isbn in ids)
        {
            Book? book = _context.Books.Find(isbn);
            if (book is null)
            {
                return NotFound();
            }

            books.Add(book);
        }

        _context.Books.RemoveRange(books);
        _context.SaveChanges();

        return NoContent();
    }
}
