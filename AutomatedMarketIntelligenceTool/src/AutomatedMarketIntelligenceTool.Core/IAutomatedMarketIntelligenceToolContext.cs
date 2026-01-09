using AutomatedMarketIntelligenceTool.Core.Models.CompetitorAggregate;
using AutomatedMarketIntelligenceTool.Core.Models.InsightAggregate;
using AutomatedMarketIntelligenceTool.Core.Models.AlertAggregate;
using AutomatedMarketIntelligenceTool.Core.Models.ReportAggregate;
using AutomatedMarketIntelligenceTool.Core.Models.DataSourceAggregate;
using AutomatedMarketIntelligenceTool.Core.Models.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace AutomatedMarketIntelligenceTool.Core;

public interface IAutomatedMarketIntelligenceToolContext
{
    DbSet<Competitor> Competitors { get; }
    DbSet<Insight> Insights { get; }
    DbSet<Alert> Alerts { get; }
    DbSet<Report> Reports { get; }
    DbSet<DataSource> DataSources { get; }
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
