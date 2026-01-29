namespace Messaging.Contracts.Events;

public sealed record GoalCreatedEvent : IntegrationEvent
{
    public required Guid GoalId { get; init; }
    public required string Name { get; init; }
    public required decimal TargetAmount { get; init; }
}
