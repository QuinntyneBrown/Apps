using Microsoft.EntityFrameworkCore;
using Rounds.Core.Models;

namespace Rounds.Core;

public interface IRoundsDbContext
{
    DbSet<Round> Rounds { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
