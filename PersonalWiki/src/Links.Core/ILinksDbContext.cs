using Microsoft.EntityFrameworkCore;
using Links.Core.Models;

namespace Links.Core;

public interface ILinksDbContext
{
    DbSet<PageLink> PageLinks { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
