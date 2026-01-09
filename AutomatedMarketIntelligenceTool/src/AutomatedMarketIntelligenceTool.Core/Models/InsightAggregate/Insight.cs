using AutomatedMarketIntelligenceTool.Core.Models.InsightAggregate.Enums;

namespace AutomatedMarketIntelligenceTool.Core.Models.InsightAggregate;

public class Insight
{
    public Guid InsightId { get; set; }
    public Guid TenantId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public InsightCategory Category { get; set; }
    public InsightImpact Impact { get; set; }
    public string? Source { get; set; }
    public string? SourceUrl { get; set; }
    public string Tags { get; set; } = string.Empty;
    public bool IsActionable { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
