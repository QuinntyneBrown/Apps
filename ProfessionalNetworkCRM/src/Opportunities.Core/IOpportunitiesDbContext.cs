using Microsoft.EntityFrameworkCore;
using Opportunities.Core.Models;

namespace Opportunities.Core;

public interface IOpportunitiesDbContext
{
    DbSet<Opportunity> Opportunitys { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
