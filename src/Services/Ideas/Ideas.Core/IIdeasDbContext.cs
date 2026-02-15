using Microsoft.EntityFrameworkCore;
using Ideas.Core.Models;

namespace Ideas.Core;

public interface IIdeasDbContext
{
    DbSet<Idea> Ideas { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
