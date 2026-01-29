namespace Milestones.Core.Models;

public class Milestone
{
    public Guid MilestoneId { get; set; }
    public Guid GoalId { get; set; }
    public Guid TenantId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? TargetDate { get; set; }
    public bool IsReached { get; set; }
    public DateTime? ReachedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
