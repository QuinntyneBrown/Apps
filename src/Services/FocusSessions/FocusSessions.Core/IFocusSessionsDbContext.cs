using Microsoft.EntityFrameworkCore;
using FocusSessions.Core.Models;

namespace FocusSessions.Core;

public interface IFocusSessionsDbContext
{
    DbSet<FocusSession> FocusSessions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
