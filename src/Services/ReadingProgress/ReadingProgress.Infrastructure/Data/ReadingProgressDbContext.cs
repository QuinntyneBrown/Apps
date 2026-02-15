using ReadingProgress.Core;
using ReadingProgress.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ReadingProgress.Infrastructure.Data;

public class ReadingProgressDbContext : DbContext, IReadingProgressDbContext
{
    public ReadingProgressDbContext(DbContextOptions<ReadingProgressDbContext> options) : base(options) { }

    public DbSet<ReadingProgress> ReadingProgresss { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ReadingProgress>(entity =>
        {
            entity.ToTable("ReadingProgresss");
            entity.HasKey(e => e.ReadingProgressId);
            entity.Property(e => e.Name).HasMaxLength(500).IsRequired();
        });
    }
}
