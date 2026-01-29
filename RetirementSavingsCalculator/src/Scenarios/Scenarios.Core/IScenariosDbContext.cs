using Microsoft.EntityFrameworkCore;
using Scenarios.Core.Models;

namespace Scenarios.Core;

public interface IScenariosDbContext
{
    DbSet<RetirementScenario> RetirementScenarios { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
