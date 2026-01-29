using Microsoft.EntityFrameworkCore;
using Schedules.Core.Models;

namespace Schedules.Core;

public interface ISchedulesDbContext
{
    DbSet<Schedule> Schedules { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
