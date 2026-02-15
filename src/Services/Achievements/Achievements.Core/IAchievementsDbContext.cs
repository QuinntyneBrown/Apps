using Microsoft.EntityFrameworkCore;
using Achievements.Core.Models;

namespace Achievements.Core;

public interface IAchievementsDbContext
{
    DbSet<Achievement> Achievements { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
