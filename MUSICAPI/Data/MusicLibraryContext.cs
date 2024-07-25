using Microsoft.EntityFrameworkCore;

public class MusicLibraryContext : DbContext
{
    public MusicLibraryContext(DbContextOptions<MusicLibraryContext> options)
        : base(options) { }

    public DbSet<Album> Albums { get; set; }
    public DbSet<Song> Songs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>().HasData(
            new Album
            {
                AlbumId = 1,
                Title = "Album 1",
                Artist = "Ruger",
                ReleaseDate = new DateTime(2020, 1, 1),
                Genre = "Genre 1"
            },
            new Album
            {
                AlbumId = 2,
                Title = "Album 2",
                Artist = "BNXN",
                ReleaseDate = new DateTime(2021, 1, 1),
                Genre = "Genre 2"
            }
        );

        modelBuilder.Entity<Song>().HasData(
            new Song
            {
                SongId = 1,
                Title = "BAE BAE",
                Duration = new TimeSpan(0, 3, 30),
                AlbumId = 1
            },
            new Song
            {
                SongId = 2,
                Title = "Ilashe",
                Duration = new TimeSpan(0, 4, 0),
                AlbumId = 2
            }
        );
    }
}
