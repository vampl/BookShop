using BookShopApi.Data;

using Microsoft.AspNetCore.Mvc;

using BookShopApi.Models;

using Microsoft.EntityFrameworkCore;

namespace BookShopApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly BookShopDbContext _context;

    public GenreController(BookShopDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Genre>> GetGenres()
    {
        return Ok(_context.Genres.ToList());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Genre> GetGenre(int id)
    {
        Genre? genre = _context.Genres.Find(id);
        if (genre is null)
        {
            return NotFound();
        }

        return Ok(genre);
    }

    [HttpPost]
    public ActionResult<Genre> PostGenre([FromBody] Genre genre)
    {
        _context.Genres.Add(genre);
        _context.SaveChanges();

        return CreatedAtAction("PostGenre", genre);
    }

    [HttpPut("{id:int}")]
    public IActionResult PutGenre(int id, [FromBody] Genre modifiedGenre)
    {
        Genre? genre = _context.Genres.Find(id);
        if (genre is null)
        {
            return NotFound();
        }

        _context.Entry(genre).CurrentValues.SetValues(modifiedGenre);
        _context.Entry(genre).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteGenre(int id)
    {
        Genre? genre = _context.Genres.Find(id);
        if (genre is null)
        {
            return NotFound();
        }

        _context.Genres.Remove(genre);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete]
    public IActionResult DeleteGenres([FromQuery] params long[] ids)
    {
        List<Genre> genres = new();
        foreach (long id in ids)
        {
            Genre? genre = _context.Genres.Find(id);
            if (genre is null)
            {
                return NotFound();
            }

            genres.Add(genre);
        }

        _context.Genres.RemoveRange(genres);
        _context.SaveChanges();

        return NoContent();
    }
}
