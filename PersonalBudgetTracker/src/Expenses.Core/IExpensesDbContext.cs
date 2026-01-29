using Microsoft.EntityFrameworkCore;

namespace Expenses.Core;

public interface IExpensesDbContext
{
    DbSet<Expense> Expenses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
