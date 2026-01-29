using Microsoft.EntityFrameworkCore;
using Gear.Core.Models;

namespace Gear.Core;

public interface IGearDbContext
{
    DbSet<GearItem> GearItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
