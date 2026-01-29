using Microsoft.EntityFrameworkCore;
using Recommendations.Core;

namespace Recommendations.Infrastructure.Data;

public class RecommendationsDbContext : DbContext, IRecommendationsDbContext
{
    public RecommendationsDbContext(DbContextOptions<RecommendationsDbContext> options) : base(options)
    {
    }

    public DbSet<Recommendation> Recommendations => Set<Recommendation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecommendationsDbContext).Assembly);
    }
}
