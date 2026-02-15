using Microsoft.EntityFrameworkCore;
using Itineraries.Core.Models;

namespace Itineraries.Core;

public interface IItinerariesDbContext
{
    DbSet<Itinerary> Itineraries { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
