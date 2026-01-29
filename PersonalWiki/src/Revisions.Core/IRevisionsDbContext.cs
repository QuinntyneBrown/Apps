using Microsoft.EntityFrameworkCore;
using Revisions.Core.Models;

namespace Revisions.Core;

public interface IRevisionsDbContext
{
    DbSet<PageRevision> PageRevisions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
