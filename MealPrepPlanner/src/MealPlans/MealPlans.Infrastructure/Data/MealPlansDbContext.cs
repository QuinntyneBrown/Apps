using MealPlans.Core;
using MealPlans.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MealPlans.Infrastructure.Data;

public class MealPlansDbContext : DbContext, IMealPlansDbContext
{
    public MealPlansDbContext(DbContextOptions<MealPlansDbContext> options) : base(options)
    {
    }

    public DbSet<MealPlan> MealPlans { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MealPlan>(entity =>
        {
            entity.ToTable("MealPlans");
            entity.HasKey(e => e.MealPlanId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
