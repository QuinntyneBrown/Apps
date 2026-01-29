using Microsoft.EntityFrameworkCore;
using ReadingProgress.Core.Models;

namespace ReadingProgress.Core;

public interface IReadingProgressDbContext
{
    DbSet<ReadingProgress> ReadingProgresss { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
