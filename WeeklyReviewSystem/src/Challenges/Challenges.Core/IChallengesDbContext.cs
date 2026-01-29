using Microsoft.EntityFrameworkCore; using Challenges.Core.Models;
namespace Challenges.Core; public interface IChallengesDbContext { DbSet<Challenge> Challenges { get; } Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); }
