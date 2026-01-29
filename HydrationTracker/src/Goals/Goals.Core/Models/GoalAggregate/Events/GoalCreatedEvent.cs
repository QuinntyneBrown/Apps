namespace Goals.Core.Models.Events;

public record GoalCreatedEvent
{
    public Guid GoalId { get; init; }
    public Guid UserId { get; init; }
    public Guid TenantId { get; init; }
    public decimal DailyGoalMl { get; init; }
    public DateTime CreatedAt { get; init; }
}
