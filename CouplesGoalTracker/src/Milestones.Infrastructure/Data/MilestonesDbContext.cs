using Milestones.Core;
using Milestones.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Milestones.Infrastructure.Data;

public class MilestonesDbContext : DbContext, IMilestonesDbContext
{
    public MilestonesDbContext(DbContextOptions<MilestonesDbContext> options) : base(options)
    {
    }

    public DbSet<Milestone> Milestones { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Milestone>(entity =>
        {
            entity.ToTable("Milestones");
            entity.HasKey(e => e.MilestoneId);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => e.GoalId);
        });
    }
}
