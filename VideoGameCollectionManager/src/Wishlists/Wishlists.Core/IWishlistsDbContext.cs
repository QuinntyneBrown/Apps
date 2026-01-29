using Microsoft.EntityFrameworkCore;
using Wishlists.Core.Models;

namespace Wishlists.Core;

public interface IWishlistsDbContext
{
    DbSet<WishlistItem> WishlistItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
