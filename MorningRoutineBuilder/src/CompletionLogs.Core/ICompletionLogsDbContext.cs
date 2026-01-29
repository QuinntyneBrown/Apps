using Microsoft.EntityFrameworkCore;
using CompletionLogs.Core.Models;

namespace CompletionLogs.Core;

public interface ICompletionLogsDbContext
{
    DbSet<CompletionLog> CompletionLogs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
