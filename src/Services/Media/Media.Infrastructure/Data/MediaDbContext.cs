using Media.Core;
using Media.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Media.Infrastructure.Data;

public class MediaDbContext : DbContext, IMediaDbContext
{
    public MediaDbContext(DbContextOptions<MediaDbContext> options) : base(options) { }

    public DbSet<FamilyPhoto> FamilyPhotos { get; set; } = null!;
    public DbSet<Story> Stories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FamilyPhoto>(e => { e.ToTable("FamilyPhotos"); e.HasKey(x => x.FamilyPhotoId); e.Property(x => x.Title).HasMaxLength(200); e.Property(x => x.Url).HasMaxLength(2000); });
        modelBuilder.Entity<Story>(e => { e.ToTable("Stories"); e.HasKey(x => x.StoryId); e.Property(x => x.Title).HasMaxLength(200); });
    }
}
