using Rewards.Core;
using Rewards.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Rewards.Infrastructure.Data;

public class RewardsDbContext : DbContext, IRewardsDbContext
{
    public RewardsDbContext(DbContextOptions<RewardsDbContext> options) : base(options)
    {
    }

    public DbSet<Reward> Rewards => Set<Reward>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Reward>(entity =>
        {
            entity.ToTable("Rewards", "rewards");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.HasIndex(e => e.Name);
        });
    }
}
