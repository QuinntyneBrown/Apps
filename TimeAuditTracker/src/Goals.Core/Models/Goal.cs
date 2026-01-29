namespace Goals.Core.Models;

public class Goal
{
    public Guid GoalId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Category { get; set; } = string.Empty;
    public int TargetMinutesPerDay { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
