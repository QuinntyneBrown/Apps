using Microsoft.EntityFrameworkCore;
using Games.Core.Models;

namespace Games.Core;

public interface IGamesDbContext
{
    DbSet<Game> Games { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
