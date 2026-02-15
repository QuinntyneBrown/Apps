using Microsoft.EntityFrameworkCore;
using Streaks.Core.Models;

namespace Streaks.Core;

public interface IStreaksDbContext
{
    DbSet<Streak> Streaks { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
