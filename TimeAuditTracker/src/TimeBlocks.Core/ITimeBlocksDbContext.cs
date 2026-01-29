using Microsoft.EntityFrameworkCore;
using TimeBlocks.Core.Models;

namespace TimeBlocks.Core;

public interface ITimeBlocksDbContext
{
    DbSet<TimeBlock> TimeBlocks { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
