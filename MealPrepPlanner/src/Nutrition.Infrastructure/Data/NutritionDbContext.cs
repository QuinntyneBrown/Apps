using Nutrition.Core;
using Nutrition.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Nutrition.Infrastructure.Data;

public class NutritionDbContext : DbContext, INutritionDbContext
{
    public NutritionDbContext(DbContextOptions<NutritionDbContext> options) : base(options)
    {
    }

    public DbSet<NutritionEntry> NutritionEntries { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<NutritionEntry>(entity =>
        {
            entity.ToTable("NutritionEntries");
            entity.HasKey(e => e.NutritionEntryId);
            entity.Property(e => e.Protein).HasPrecision(18, 2);
            entity.Property(e => e.Carbohydrates).HasPrecision(18, 2);
            entity.Property(e => e.Fat).HasPrecision(18, 2);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
