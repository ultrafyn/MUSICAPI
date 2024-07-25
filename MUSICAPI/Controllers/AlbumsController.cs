using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class AlbumsController : ControllerBase
{
    private readonly MusicLibraryContext _context;

    public AlbumsController(MusicLibraryContext context)
    {
        _context = context;
    }

    // GET: api/Albums
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Album>>> GetAlbums()
    {
        return await _context.Albums.Include(a => a.Songs).ToListAsync();
    }

    // GET: api/Albums/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Album>> GetAlbum(int id)
    {
        var album = await _context.Albums.Include(a => a.Songs).FirstOrDefaultAsync(a => a.AlbumId == id);

        if (album == null)
        {
            return NotFound();
        }

        return album;
    }

    // POST: api/Albums
    [HttpPost]
    public async Task<ActionResult<Album>> PostAlbum(Album album)
    {
        _context.Albums.Add(album);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAlbum), new { id = album.AlbumId }, album);
    }

    // PUT: api/Albums/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAlbum(int id, Album album)
    {
        if (id != album.AlbumId)
        {
            return BadRequest();
        }

        _context.Entry(album).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Albums.Any(e => e.AlbumId == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Albums/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlbum(int id)
    {
        var album = await _context.Albums.FindAsync(id);
        if (album == null)
        {
            return NotFound();
        }

        _context.Albums.Remove(album);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
