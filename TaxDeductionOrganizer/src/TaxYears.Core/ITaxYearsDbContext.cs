using Microsoft.EntityFrameworkCore;
using TaxYears.Core.Models;

namespace TaxYears.Core;

public interface ITaxYearsDbContext
{
    DbSet<TaxYear> TaxYears { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
