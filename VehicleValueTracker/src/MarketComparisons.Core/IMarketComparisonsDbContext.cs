using Microsoft.EntityFrameworkCore;
using MarketComparisons.Core.Models;

namespace MarketComparisons.Core;

public interface IMarketComparisonsDbContext
{
    DbSet<MarketComparison> MarketComparisons { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
