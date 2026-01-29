using Microsoft.EntityFrameworkCore;
using Donations.Core.Models;

namespace Donations.Core;

public interface IDonationsDbContext
{
    DbSet<Donation> Donations { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
