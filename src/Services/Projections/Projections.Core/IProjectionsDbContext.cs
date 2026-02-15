using Microsoft.EntityFrameworkCore;
using Projections.Core.Models;
namespace Projections.Core;
public interface IProjectionsDbContext
{
    DbSet<Projection> Projections { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
