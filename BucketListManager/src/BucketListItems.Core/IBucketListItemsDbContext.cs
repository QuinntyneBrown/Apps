using Microsoft.EntityFrameworkCore;
using BucketListItems.Core.Models;

namespace BucketListItems.Core;

public interface IBucketListItemsDbContext
{
    DbSet<BucketListItem> BucketListItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
