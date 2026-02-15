using GiftPlanning.Core;
using GiftPlanning.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftPlanning.Infrastructure.Data;

public class GiftPlanningDbContext : DbContext, IGiftPlanningDbContext
{
    public GiftPlanningDbContext(DbContextOptions<GiftPlanningDbContext> options) : base(options)
    {
    }

    public DbSet<GiftPlan> GiftPlans { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GiftPlan>(entity =>
        {
            entity.ToTable("GiftPlans");
            entity.HasKey(e => e.GiftPlanId);
            entity.Property(e => e.RecipientName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.GiftIdea).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Budget).HasPrecision(18, 2);
        });
    }
}
