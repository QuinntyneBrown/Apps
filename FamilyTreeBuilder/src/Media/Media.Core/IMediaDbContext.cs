using Microsoft.EntityFrameworkCore;
using Media.Core.Models;

namespace Media.Core;

public interface IMediaDbContext
{
    DbSet<FamilyPhoto> FamilyPhotos { get; }
    DbSet<Story> Stories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
