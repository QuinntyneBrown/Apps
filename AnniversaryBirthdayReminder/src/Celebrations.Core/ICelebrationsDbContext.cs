using Microsoft.EntityFrameworkCore;
using Celebrations.Core.Models;

namespace Celebrations.Core;

public interface ICelebrationsDbContext
{
    DbSet<Celebration> Celebrations { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
