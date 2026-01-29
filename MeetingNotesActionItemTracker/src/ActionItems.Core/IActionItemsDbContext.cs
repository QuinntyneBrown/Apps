using Microsoft.EntityFrameworkCore;
using ActionItems.Core.Models;

namespace ActionItems.Core;

public interface IActionItemsDbContext
{
    DbSet<ActionItem> ActionItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
