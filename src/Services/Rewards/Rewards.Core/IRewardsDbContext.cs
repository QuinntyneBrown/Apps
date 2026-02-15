using Microsoft.EntityFrameworkCore;
using Rewards.Core.Models;

namespace Rewards.Core;

public interface IRewardsDbContext
{
    DbSet<Reward> Rewards { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
