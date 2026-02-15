namespace Milestones.Api.Features;

public record MilestoneDto
{
    public Guid MilestoneId { get; init; }
    public Guid GoalId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal TargetAmount { get; init; }
    public bool IsReached { get; init; }
    public DateTime? ReachedAt { get; init; }
    public DateTime CreatedAt { get; init; }
}
