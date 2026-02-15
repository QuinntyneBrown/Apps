using Microsoft.EntityFrameworkCore;
using Readings.Core.Models;

namespace Readings.Core;

public interface IReadingsDbContext
{
    DbSet<Reading> Readings { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
