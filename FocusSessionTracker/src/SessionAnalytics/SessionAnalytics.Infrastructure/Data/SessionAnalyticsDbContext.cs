using SessionAnalytics.Core;
using SessionAnalytics.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace SessionAnalytics.Infrastructure.Data;

public class SessionAnalyticsDbContext : DbContext, ISessionAnalyticsDbContext
{
    public SessionAnalyticsDbContext(DbContextOptions<SessionAnalyticsDbContext> options) : base(options)
    {
    }

    public DbSet<Analytics> Analytics { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Analytics>(entity =>
        {
            entity.ToTable("SessionAnalytics");
            entity.HasKey(e => e.AnalyticsId);
            entity.HasIndex(e => new { e.UserId, e.Date }).IsUnique();
            entity.HasIndex(e => e.TenantId);
        });
    }
}
