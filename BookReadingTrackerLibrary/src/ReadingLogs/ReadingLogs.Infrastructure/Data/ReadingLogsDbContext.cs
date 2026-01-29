using ReadingLogs.Core;
using ReadingLogs.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ReadingLogs.Infrastructure.Data;

public class ReadingLogsDbContext : DbContext, IReadingLogsDbContext
{
    public ReadingLogsDbContext(DbContextOptions<ReadingLogsDbContext> options) : base(options)
    {
    }

    public DbSet<ReadingLog> ReadingLogs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ReadingLog>(entity =>
        {
            entity.ToTable("ReadingLogs");
            entity.HasKey(e => e.ReadingLogId);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
            entity.HasIndex(e => e.BookId);
            entity.HasIndex(e => e.ReadAt);
        });
    }
}
