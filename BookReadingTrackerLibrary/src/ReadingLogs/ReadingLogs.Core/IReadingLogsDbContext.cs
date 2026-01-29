using Microsoft.EntityFrameworkCore;
using ReadingLogs.Core.Models;

namespace ReadingLogs.Core;

public interface IReadingLogsDbContext
{
    DbSet<ReadingLog> ReadingLogs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
