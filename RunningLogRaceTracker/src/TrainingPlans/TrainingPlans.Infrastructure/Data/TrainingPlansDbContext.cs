using Microsoft.EntityFrameworkCore;
using TrainingPlans.Core;
using TrainingPlans.Core.Models;

namespace TrainingPlans.Infrastructure.Data;

public class TrainingPlansDbContext : DbContext, ITrainingPlansDbContext
{
    public TrainingPlansDbContext(DbContextOptions<TrainingPlansDbContext> options) : base(options) { }

    public DbSet<TrainingPlan> TrainingPlans { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TrainingPlan>(entity =>
        {
            entity.ToTable("TrainingPlans");
            entity.HasKey(e => e.TrainingPlanId);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.WeeklyMileageGoal).HasPrecision(10, 2);
            entity.Property(e => e.Notes).HasMaxLength(2000);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
