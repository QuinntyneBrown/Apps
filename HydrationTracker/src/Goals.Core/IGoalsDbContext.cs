using Microsoft.EntityFrameworkCore;
using Goals.Core.Models;

namespace Goals.Core;

public interface IGoalsDbContext
{
    DbSet<Goal> Goals { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
