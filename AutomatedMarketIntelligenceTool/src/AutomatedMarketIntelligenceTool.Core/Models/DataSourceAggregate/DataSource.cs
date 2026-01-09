using AutomatedMarketIntelligenceTool.Core.Models.DataSourceAggregate.Enums;

namespace AutomatedMarketIntelligenceTool.Core.Models.DataSourceAggregate;

public class DataSource
{
    public Guid DataSourceId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DataSourceType Type { get; set; }
    public string? Url { get; set; }
    public int ReliabilityScore { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastFetched { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
