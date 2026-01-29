using Microsoft.EntityFrameworkCore;

namespace Budgets.Core;

public interface IBudgetsDbContext
{
    DbSet<Budget> Budgets { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
