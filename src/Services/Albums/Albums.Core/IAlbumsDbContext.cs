using Microsoft.EntityFrameworkCore;
using Albums.Core.Models;

namespace Albums.Core;

public interface IAlbumsDbContext
{
    DbSet<Album> Albums { get; }
    DbSet<Photo> Photos { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
