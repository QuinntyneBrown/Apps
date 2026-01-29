using Microsoft.EntityFrameworkCore;
using Routines.Core.Models;

namespace Routines.Core;

public interface IRoutinesDbContext
{
    DbSet<Routine> Routines { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
