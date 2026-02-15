using Microsoft.EntityFrameworkCore;
using TaxReports.Core.Models;

namespace TaxReports.Core;

public interface ITaxReportsDbContext
{
    DbSet<TaxReport> TaxReports { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
