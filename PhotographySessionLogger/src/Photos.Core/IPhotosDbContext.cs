using Microsoft.EntityFrameworkCore;
using Photos.Core.Models;

namespace Photos.Core;

public interface IPhotosDbContext
{
    DbSet<Photo> Photos { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
