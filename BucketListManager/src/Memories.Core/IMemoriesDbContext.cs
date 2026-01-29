using Microsoft.EntityFrameworkCore;
using Memories.Core.Models;

namespace Memories.Core;

public interface IMemoriesDbContext
{
    DbSet<Memory> Memories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
