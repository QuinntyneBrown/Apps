using Microsoft.EntityFrameworkCore;
using Reflections.Core.Models;

namespace Reflections.Core;

public interface IReflectionsDbContext
{
    DbSet<Reflection> Reflections { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
