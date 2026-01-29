using Microsoft.EntityFrameworkCore;
using Vehicles.Core.Models;

namespace Vehicles.Core;

public interface IVehiclesDbContext
{
    DbSet<Vehicle> Vehicles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
