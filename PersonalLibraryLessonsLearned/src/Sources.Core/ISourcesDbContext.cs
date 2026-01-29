using Microsoft.EntityFrameworkCore;
using Sources.Core.Models;

namespace Sources.Core;

public interface ISourcesDbContext
{
    DbSet<Source> Sources { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
