using Microsoft.EntityFrameworkCore;
using Expenses.Core.Models;

namespace Expenses.Core;

public interface IExpensesDbContext
{
    DbSet<Expense> Expenses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
