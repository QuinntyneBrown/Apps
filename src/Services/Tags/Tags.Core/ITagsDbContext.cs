using Microsoft.EntityFrameworkCore;
using Tags.Core.Models;

namespace Tags.Core;

public interface ITagsDbContext
{
    DbSet<Tag> Tags { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
