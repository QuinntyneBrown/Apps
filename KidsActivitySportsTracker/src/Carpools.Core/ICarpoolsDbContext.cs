using Microsoft.EntityFrameworkCore;
using Carpools.Core.Models;

namespace Carpools.Core;

public interface ICarpoolsDbContext
{
    DbSet<Carpool> Carpools { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
