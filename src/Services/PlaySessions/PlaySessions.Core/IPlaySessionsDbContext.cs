using Microsoft.EntityFrameworkCore;
using PlaySessions.Core.Models;

namespace PlaySessions.Core;

public interface IPlaySessionsDbContext
{
    DbSet<PlaySession> PlaySessions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
