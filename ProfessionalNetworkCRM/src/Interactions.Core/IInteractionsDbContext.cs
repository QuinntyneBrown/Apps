using Microsoft.EntityFrameworkCore;
using Interactions.Core.Models;

namespace Interactions.Core;

public interface IInteractionsDbContext
{
    DbSet<Interaction> Interactions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
