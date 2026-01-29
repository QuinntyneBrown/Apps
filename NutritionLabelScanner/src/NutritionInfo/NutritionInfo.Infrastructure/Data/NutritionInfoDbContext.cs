using Microsoft.EntityFrameworkCore;
using NutritionInfo.Core;

namespace NutritionInfo.Infrastructure.Data;

public class NutritionInfoDbContext : DbContext, INutritionInfoDbContext
{
    public NutritionInfoDbContext(DbContextOptions<NutritionInfoDbContext> options) : base(options)
    {
    }

    public DbSet<NutritionData> NutritionData => Set<NutritionData>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NutritionInfoDbContext).Assembly);
    }
}
