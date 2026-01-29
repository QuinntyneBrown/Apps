using Microsoft.EntityFrameworkCore;
using Scheduling.Core.Models;

namespace Scheduling.Core;

public interface ISchedulingDbContext
{
    DbSet<Availability> Availabilities { get; }
    DbSet<Reminder> Reminders { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
