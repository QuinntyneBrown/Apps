using Microsoft.EntityFrameworkCore;
using Distractions.Core.Models;

namespace Distractions.Core;

public interface IDistractionsDbContext
{
    DbSet<Distraction> Distractions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
