using Goals.Core;
using Goals.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Goals.Infrastructure.Data;

public class GoalsDbContext : DbContext, IGoalsDbContext
{
    public GoalsDbContext(DbContextOptions<GoalsDbContext> options) : base(options)
    {
    }

    public DbSet<Goal> Goals { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.ToTable("Goals");
            entity.HasKey(e => e.GoalId);
            entity.Property(e => e.DailyGoalMl).HasPrecision(18, 2);
        });
    }
}
