using Strategies.Core;
using Strategies.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Strategies.Infrastructure.Data;

public class StrategiesDbContext : DbContext, IStrategiesDbContext
{
    public StrategiesDbContext(DbContextOptions<StrategiesDbContext> options) : base(options) { }

    public DbSet<WithdrawalStrategy> WithdrawalStrategies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<WithdrawalStrategy>(entity =>
        {
            entity.ToTable("WithdrawalStrategies");
            entity.HasKey(e => e.WithdrawalStrategyId);
            entity.Property(e => e.WithdrawalRate).HasPrecision(5, 2);
            entity.Property(e => e.AnnualWithdrawalAmount).HasPrecision(18, 2);
            entity.Property(e => e.MinimumBalance).HasPrecision(18, 2);
        });
    }
}
