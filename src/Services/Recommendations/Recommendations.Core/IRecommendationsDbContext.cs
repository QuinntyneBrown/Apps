using Microsoft.EntityFrameworkCore;

namespace Recommendations.Core;

public interface IRecommendationsDbContext
{
    DbSet<Recommendation> Recommendations { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
