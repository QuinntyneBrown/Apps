using Microsoft.EntityFrameworkCore;
using Referrals.Core.Models;

namespace Referrals.Core;

public interface IReferralsDbContext
{
    DbSet<Referral> Referrals { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
