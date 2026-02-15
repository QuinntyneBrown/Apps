using Microsoft.EntityFrameworkCore;
using Meetings.Core.Models;

namespace Meetings.Core;

public interface IMeetingsDbContext
{
    DbSet<Meeting> Meetings { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
