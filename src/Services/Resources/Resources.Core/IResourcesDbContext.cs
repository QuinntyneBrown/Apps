using Microsoft.EntityFrameworkCore;
using Resources.Core.Models;

namespace Resources.Core;

public interface IResourcesDbContext
{
    DbSet<Resource> Resources { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
