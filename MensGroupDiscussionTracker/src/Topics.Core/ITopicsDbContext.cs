using Microsoft.EntityFrameworkCore;
using Topics.Core.Models;

namespace Topics.Core;

public interface ITopicsDbContext
{
    DbSet<Topic> Topics { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
