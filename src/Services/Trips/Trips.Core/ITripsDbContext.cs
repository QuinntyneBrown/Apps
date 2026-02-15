using Microsoft.EntityFrameworkCore;
using Trips.Core.Models;

namespace Trips.Core;

public interface ITripsDbContext
{
    DbSet<Trip> Trips { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
