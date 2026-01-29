namespace Progresses.Core.Models;

public class Progress
{
    public Guid ProgressId { get; set; }
    public Guid GoalId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public decimal PercentageComplete { get; set; }
    public string? Notes { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
}
