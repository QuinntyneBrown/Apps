using Microsoft.EntityFrameworkCore;
using TaxEstimates.Core.Models;

namespace TaxEstimates.Core;

public interface ITaxEstimatesDbContext
{
    DbSet<TaxEstimate> TaxEstimates { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
