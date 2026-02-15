using Microsoft.EntityFrameworkCore;
using Manuals.Core.Models;

namespace Manuals.Core;

public interface IManualsDbContext
{
    DbSet<Manual> Manuals { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
