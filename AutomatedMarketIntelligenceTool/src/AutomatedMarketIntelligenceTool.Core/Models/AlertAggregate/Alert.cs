using AutomatedMarketIntelligenceTool.Core.Models.AlertAggregate.Enums;

namespace AutomatedMarketIntelligenceTool.Core.Models.AlertAggregate;

public class Alert
{
    public Guid AlertId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public AlertType AlertType { get; set; }
    public bool IsActive { get; set; }
    public string? Criteria { get; set; }
    public NotificationPreference NotificationPreference { get; set; }
    public DateTime? LastTriggered { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
