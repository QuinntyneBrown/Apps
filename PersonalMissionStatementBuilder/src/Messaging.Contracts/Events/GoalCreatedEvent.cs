namespace Messaging.Contracts.Events;

public record GoalCreatedEvent : IntegrationEvent
{
    public Guid GoalId { get; init; }
    public Guid MissionStatementId { get; init; }
    public required string Title { get; init; }
    public required string Category { get; init; }
    public DateTime TargetDate { get; init; }
}
