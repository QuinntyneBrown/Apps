using Microsoft.EntityFrameworkCore;

namespace Comparisons.Core;

public interface IComparisonsDbContext
{
    DbSet<Comparison> Comparisons { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
