using Microsoft.EntityFrameworkCore;
using SessionAnalytics.Core.Models;

namespace SessionAnalytics.Core;

public interface ISessionAnalyticsDbContext
{
    DbSet<Analytics> Analytics { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
