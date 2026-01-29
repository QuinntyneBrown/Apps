using Microsoft.EntityFrameworkCore;
using Strategies.Core.Models;

namespace Strategies.Core;

public interface IStrategiesDbContext
{
    DbSet<WithdrawalStrategy> WithdrawalStrategies { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
