using Microsoft.EntityFrameworkCore;

namespace Ratings.Core;

public interface IRatingsDbContext
{
    DbSet<Rating> Ratings { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
