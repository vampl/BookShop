using BookShopApi.Data;

using Microsoft.AspNetCore.Mvc;

using BookShopApi.Models;

using Microsoft.EntityFrameworkCore;

namespace BookShopApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly BookShopDbContext _context;

    public GenresController(BookShopDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Genre>> GetGenres()
    {
        return Ok(_context.Genres.ToList());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<Genre> PostGenre([FromBody] Genre genre)
    {
        _context.Genres.Add(genre);
        _context.SaveChanges();

        return CreatedAtAction("PostGenre", genre);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteGenres([FromQuery] params int[] ids)
    {
        List<Genre> genres = new();
        foreach (int id in ids)
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
