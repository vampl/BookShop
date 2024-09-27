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

    [HttpGet("{id:int}")]
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
    public ActionResult<Book> PostBook([FromBody] Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();

        return CreatedAtAction("PostBook", book);
    }

    [HttpPut("{id:int}")]
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
}
