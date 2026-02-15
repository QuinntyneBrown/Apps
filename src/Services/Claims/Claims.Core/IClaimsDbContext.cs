using Microsoft.EntityFrameworkCore;
using Claims.Core.Models;

namespace Claims.Core;

public interface IClaimsDbContext
{
    DbSet<Claim> Claims { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
