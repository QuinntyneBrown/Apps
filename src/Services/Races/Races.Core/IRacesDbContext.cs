using Microsoft.EntityFrameworkCore;
using Races.Core.Models;

namespace Races.Core;

public interface IRacesDbContext
{
    DbSet<Race> Races { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
