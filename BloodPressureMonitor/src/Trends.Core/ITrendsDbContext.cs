using Microsoft.EntityFrameworkCore;
using Trends.Core.Models;

namespace Trends.Core;

public interface ITrendsDbContext
{
    DbSet<Trend> Trends { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
