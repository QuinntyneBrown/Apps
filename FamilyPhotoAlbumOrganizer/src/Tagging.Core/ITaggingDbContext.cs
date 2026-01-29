using Microsoft.EntityFrameworkCore;
using Tagging.Core.Models;

namespace Tagging.Core;

public interface ITaggingDbContext
{
    DbSet<Tag> Tags { get; }
    DbSet<PersonTag> PersonTags { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
