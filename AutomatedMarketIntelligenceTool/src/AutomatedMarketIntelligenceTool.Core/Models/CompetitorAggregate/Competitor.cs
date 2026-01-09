using AutomatedMarketIntelligenceTool.Core.Models.CompetitorAggregate.Enums;

namespace AutomatedMarketIntelligenceTool.Core.Models.CompetitorAggregate;

public class Competitor
{
    public Guid CompetitorId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Industry { get; set; }
    public string? Website { get; set; }
    public string? Description { get; set; }
    public int? EmployeeCount { get; set; }
    public decimal? AnnualRevenue { get; set; }
    public MarketPosition MarketPosition { get; set; }
    public string? Strengths { get; set; }
    public string? Weaknesses { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
