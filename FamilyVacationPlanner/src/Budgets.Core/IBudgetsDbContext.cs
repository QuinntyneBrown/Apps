using Microsoft.EntityFrameworkCore;
using Budgets.Core.Models;

namespace Budgets.Core;

public interface IBudgetsDbContext
{
    DbSet<VacationBudget> VacationBudgets { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
