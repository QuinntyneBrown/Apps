using Microsoft.EntityFrameworkCore;
using Intakes.Core.Models;

namespace Intakes.Core;

public interface IIntakesDbContext
{
    DbSet<Intake> Intakes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
