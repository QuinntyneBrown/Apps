using Microsoft.EntityFrameworkCore;

namespace Neighbors.Core;

public interface INeighborsDbContext
{
    DbSet<Neighbor> Neighbors { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
