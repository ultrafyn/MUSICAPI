using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class SongsController : ControllerBase
{
    private readonly MusicLibraryContext _context;

    public SongsController(MusicLibraryContext context)
    {
        _context = context;
    }

    // GET: api/Songs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
    {
        return await _context.Songs.Include(s => s.Album).ToListAsync();
    }

    // GET: api/Songs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Song>> GetSong(int id)
    {
        var song = await _context.Songs.Include(s => s.Album).FirstOrDefaultAsync(s => s.SongId == id);

        if (song == null)
        {
            return NotFound();
        }

        return song;
    }

    // POST: api/Songs
    [HttpPost]
    public async Task<ActionResult<Song>> PostSong(Song song)
    {
        _context.Songs.Add(song);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSong), new { id = song.SongId }, song);
    }

    // PUT: api/Songs/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSong(int id, Song song)
    {
        if (id != song.SongId)
        {
            return BadRequest();
        }

        _context.Entry(song).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Songs.Any(e => e.SongId == id))
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

    // DELETE: api/Songs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSong(int id)
    {
        var song = await _context.Songs.FindAsync(id);
        if (song == null)
        {
            return NotFound();
        }

        _context.Songs.Remove(song);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
