using Microsoft.EntityFrameworkCore;
using FollowUps.Core.Models;

namespace FollowUps.Core;

public interface IFollowUpsDbContext
{
    DbSet<FollowUp> FollowUps { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
