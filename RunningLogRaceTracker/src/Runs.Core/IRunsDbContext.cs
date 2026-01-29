using Microsoft.EntityFrameworkCore;
using Runs.Core.Models;

namespace Runs.Core;

public interface IRunsDbContext
{
    DbSet<Run> Runs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
