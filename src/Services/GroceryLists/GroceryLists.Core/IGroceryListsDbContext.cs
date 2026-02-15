using Microsoft.EntityFrameworkCore;
using GroceryLists.Core.Models;

namespace GroceryLists.Core;

public interface IGroceryListsDbContext
{
    DbSet<GroceryList> GroceryLists { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
