namespace Messaging.Contracts.Events;

public sealed record MilestoneReachedEvent : IntegrationEvent
{
    public required Guid MilestoneId { get; init; }
    public required Guid GoalId { get; init; }
    public required string Name { get; init; }
}
