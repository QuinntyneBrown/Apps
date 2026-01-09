using AutomatedMarketIntelligenceTool.Core;
using AutomatedMarketIntelligenceTool.Core.Models.CompetitorAggregate;
using AutomatedMarketIntelligenceTool.Core.Models.InsightAggregate;
using AutomatedMarketIntelligenceTool.Core.Models.AlertAggregate;
using AutomatedMarketIntelligenceTool.Core.Models.ReportAggregate;
using AutomatedMarketIntelligenceTool.Core.Models.DataSourceAggregate;
using AutomatedMarketIntelligenceTool.Core.Models.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace AutomatedMarketIntelligenceTool.Infrastructure;

public class AutomatedMarketIntelligenceToolContext : DbContext, IAutomatedMarketIntelligenceToolContext
{
    private readonly ITenantContext _tenantContext;

    public AutomatedMarketIntelligenceToolContext(
        DbContextOptions<AutomatedMarketIntelligenceToolContext> options,
        ITenantContext tenantContext)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    public DbSet<Competitor> Competitors => Set<Competitor>();
    public DbSet<Insight> Insights => Set<Insight>();
    public DbSet<Alert> Alerts => Set<Alert>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<DataSource> DataSources => Set<DataSource>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Competitor>(entity =>
        {
            entity.HasKey(e => e.CompetitorId);
            entity.HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        });

        modelBuilder.Entity<Insight>(entity =>
        {
            entity.HasKey(e => e.InsightId);
            entity.HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        });

        modelBuilder.Entity<Alert>(entity =>
        {
            entity.HasKey(e => e.AlertId);
            entity.HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId);
            entity.HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        });

        modelBuilder.Entity<DataSource>(entity =>
        {
            entity.HasKey(e => e.DataSourceId);
            entity.HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        });
    }
}
