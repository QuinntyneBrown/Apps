using Microsoft.EntityFrameworkCore;

namespace Events.Core;

public interface IEventsDbContext
{
    DbSet<Event> Events { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
