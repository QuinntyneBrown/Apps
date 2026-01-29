using Microsoft.EntityFrameworkCore;
using Contributions.Core.Models;

namespace Contributions.Core;

public interface IContributionsDbContext
{
    DbSet<Contribution> Contributions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
