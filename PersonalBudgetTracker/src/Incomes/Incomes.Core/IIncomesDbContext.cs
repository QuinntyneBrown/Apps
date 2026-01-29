using Microsoft.EntityFrameworkCore;

namespace Incomes.Core;

public interface IIncomesDbContext
{
    DbSet<Income> Incomes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
