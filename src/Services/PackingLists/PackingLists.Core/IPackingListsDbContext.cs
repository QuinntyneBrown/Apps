using Microsoft.EntityFrameworkCore;
using PackingLists.Core.Models;

namespace PackingLists.Core;

public interface IPackingListsDbContext
{
    DbSet<PackingList> PackingLists { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
