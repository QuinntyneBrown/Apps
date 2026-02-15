using Microsoft.EntityFrameworkCore;
using Events.Core.Models;

namespace Events.Core;

public interface IEventsDbContext
{
    DbSet<CalendarEvent> CalendarEvents { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
