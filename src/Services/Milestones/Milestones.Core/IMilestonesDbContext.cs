using Microsoft.EntityFrameworkCore;
using Milestones.Core.Models;

namespace Milestones.Core;

public interface IMilestonesDbContext
{
    DbSet<Milestone> Milestones { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
