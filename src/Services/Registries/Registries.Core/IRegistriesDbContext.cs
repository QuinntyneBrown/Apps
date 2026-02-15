using Microsoft.EntityFrameworkCore;
using Registries.Core.Models;

namespace Registries.Core;

public interface IRegistriesDbContext
{
    DbSet<Registry> Registries { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
