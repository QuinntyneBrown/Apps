using Microsoft.EntityFrameworkCore;
using Watchlists.Core;

namespace Watchlists.Infrastructure.Data;

public class WatchlistsDbContext : DbContext, IWatchlistsDbContext
{
    public WatchlistsDbContext(DbContextOptions<WatchlistsDbContext> options) : base(options)
    {
    }

    public DbSet<WatchlistItem> WatchlistItems => Set<WatchlistItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WatchlistsDbContext).Assembly);
    }
}
