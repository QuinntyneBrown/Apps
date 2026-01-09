using AutomatedMarketIntelligenceTool.Core.Models.ReportAggregate.Enums;

namespace AutomatedMarketIntelligenceTool.Core.Models.ReportAggregate;

public class Report
{
    public Guid ReportId { get; set; }
    public Guid TenantId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ReportType ReportType { get; set; }
    public DateTime GeneratedAt { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
}
