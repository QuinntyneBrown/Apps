using Albums.Core;
using Albums.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Albums.Infrastructure.Data;

public class AlbumsDbContext : DbContext, IAlbumsDbContext
{
    public AlbumsDbContext(DbContextOptions<AlbumsDbContext> options) : base(options) { }

    public DbSet<Album> Albums { get; set; } = null!;
    public DbSet<Photo> Photos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(e => { e.ToTable("Albums"); e.HasKey(x => x.AlbumId); e.Property(x => x.Name).HasMaxLength(200); });
        modelBuilder.Entity<Photo>(e => { e.ToTable("Photos"); e.HasKey(x => x.PhotoId); e.Property(x => x.FileName).HasMaxLength(500); e.Property(x => x.Url).HasMaxLength(2000); });
    }
}
