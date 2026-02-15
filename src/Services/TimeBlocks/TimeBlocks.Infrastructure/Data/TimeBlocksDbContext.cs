using TimeBlocks.Core;
using TimeBlocks.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace TimeBlocks.Infrastructure.Data;

public class TimeBlocksDbContext : DbContext, ITimeBlocksDbContext
{
    public TimeBlocksDbContext(DbContextOptions<TimeBlocksDbContext> options) : base(options)
    {
    }

    public DbSet<TimeBlock> TimeBlocks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TimeBlock>(entity =>
        {
            entity.ToTable("TimeBlocks");
            entity.HasKey(e => e.TimeBlockId);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Category).HasConversion<string>();
            entity.Property(e => e.Tags).HasMaxLength(500);
            entity.HasIndex(e => new { e.TenantId, e.UserId, e.StartTime });
        });
    }
}
