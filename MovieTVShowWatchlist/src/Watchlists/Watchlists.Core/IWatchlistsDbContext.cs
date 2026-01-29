using Microsoft.EntityFrameworkCore;

namespace Watchlists.Core;

public interface IWatchlistsDbContext
{
    DbSet<WatchlistItem> WatchlistItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
