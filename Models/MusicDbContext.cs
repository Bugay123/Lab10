using Microsoft.EntityFrameworkCore;

namespace Lab10.Models
{
    public class MusicDbContext(DbContextOptions<MusicDbContext> options) : DbContext(options)
    {
        public DbSet<MusicTrack> MusicTracks { get; set; } = default!;
    }
}
