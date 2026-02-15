using Microsoft.EntityFrameworkCore;
using Handicaps.Core.Models;

namespace Handicaps.Core;

public interface IHandicapsDbContext
{
    DbSet<Handicap> Handicaps { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
