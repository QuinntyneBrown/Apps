using Microsoft.EntityFrameworkCore;
using Receipts.Core.Models;

namespace Receipts.Core;

public interface IReceiptsDbContext
{
    DbSet<Receipt> Receipts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
