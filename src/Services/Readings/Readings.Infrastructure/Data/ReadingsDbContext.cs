using Readings.Core;
using Readings.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Readings.Infrastructure.Data;

public class ReadingsDbContext : DbContext, IReadingsDbContext
{
    public ReadingsDbContext(DbContextOptions<ReadingsDbContext> options) : base(options)
    {
    }

    public DbSet<Reading> Readings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Reading>(entity =>
        {
            entity.ToTable("Readings");
            entity.HasKey(e => e.ReadingId);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
            entity.HasIndex(e => e.RecordedAt);
        });
    }
}
