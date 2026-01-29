using Microsoft.EntityFrameworkCore;
using Favorites.Core.Models;

namespace Favorites.Core;

public interface IFavoritesDbContext
{
    DbSet<Favorite> Favorites { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
