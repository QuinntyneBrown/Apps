using Microsoft.EntityFrameworkCore;
using RegistryItems.Core.Models;

namespace RegistryItems.Core;

public interface IRegistryItemsDbContext
{
    DbSet<RegistryItem> RegistryItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
