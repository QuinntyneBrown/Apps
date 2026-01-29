namespace Goals.Core.Models;

public class Goal
{
    public Guid GoalId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Category { get; set; } = string.Empty;
    public DateTime? TargetDate { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
