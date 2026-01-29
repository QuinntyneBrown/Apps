using MarketComparisons.Core;
using MarketComparisons.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketComparisons.Infrastructure.Data;

public class MarketComparisonsDbContext : DbContext, IMarketComparisonsDbContext
{
    public MarketComparisonsDbContext(DbContextOptions<MarketComparisonsDbContext> options) : base(options)
    {
    }

    public DbSet<MarketComparison> MarketComparisons { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MarketComparison>(entity =>
        {
            entity.ToTable("MarketComparisons");
            entity.HasKey(e => e.ComparisonId);
            entity.Property(e => e.MarketPrice).HasPrecision(18, 2);
            entity.Property(e => e.Source).HasMaxLength(200);
        });
    }
}
