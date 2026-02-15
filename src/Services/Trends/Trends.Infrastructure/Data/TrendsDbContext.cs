using Trends.Core;
using Trends.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Trends.Infrastructure.Data;

public class TrendsDbContext : DbContext, ITrendsDbContext
{
    public TrendsDbContext(DbContextOptions<TrendsDbContext> options) : base(options)
    {
    }

    public DbSet<Trend> Trends { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Trend>(entity =>
        {
            entity.ToTable("Trends");
            entity.HasKey(e => e.TrendId);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
            entity.Property(e => e.AverageSystolic).HasPrecision(18, 2);
            entity.Property(e => e.AverageDiastolic).HasPrecision(18, 2);
            entity.Property(e => e.AveragePulse).HasPrecision(18, 2);
        });
    }
}
