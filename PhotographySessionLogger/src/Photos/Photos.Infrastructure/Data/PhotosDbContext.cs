using Photos.Core;
using Photos.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Photos.Infrastructure.Data;

public class PhotosDbContext : DbContext, IPhotosDbContext
{
    public PhotosDbContext(DbContextOptions<PhotosDbContext> options) : base(options)
    {
    }

    public DbSet<Photo> Photos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.ToTable("Photos");
            entity.HasKey(e => e.PhotoId);
            entity.Property(e => e.FileName).HasMaxLength(500).IsRequired();
            entity.Property(e => e.FilePath).HasMaxLength(1000);
            entity.Property(e => e.Tags).HasMaxLength(1000);
        });
    }
}
