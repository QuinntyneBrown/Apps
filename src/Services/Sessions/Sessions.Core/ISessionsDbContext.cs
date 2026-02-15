using Microsoft.EntityFrameworkCore;
using Sessions.Core.Models;

namespace Sessions.Core;

public interface ISessionsDbContext
{
    DbSet<Session> Sessions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
