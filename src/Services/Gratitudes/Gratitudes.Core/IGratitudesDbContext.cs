using Microsoft.EntityFrameworkCore;
using Gratitudes.Core.Models;

namespace Gratitudes.Core;

public interface IGratitudesDbContext
{
    DbSet<Gratitude> Gratitudes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
