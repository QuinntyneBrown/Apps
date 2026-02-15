using Microsoft.EntityFrameworkCore;
using Chores.Core.Models;

namespace Chores.Core;

public interface IChoresDbContext
{
    DbSet<Chore> Chores { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
