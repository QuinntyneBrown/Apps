using Microsoft.EntityFrameworkCore;
using Appliances.Core.Models;

namespace Appliances.Core;

public interface IAppliancesDbContext
{
    DbSet<Appliance> Appliances { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
