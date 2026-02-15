using Microsoft.EntityFrameworkCore;
using Activities.Core.Models;

namespace Activities.Core;

public interface IActivitiesDbContext
{
    DbSet<Activity> Activities { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
