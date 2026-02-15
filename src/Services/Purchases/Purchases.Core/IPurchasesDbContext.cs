using Microsoft.EntityFrameworkCore;
using Purchases.Core.Models;

namespace Purchases.Core;

public interface IPurchasesDbContext
{
    DbSet<Purchase> Purchases { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
