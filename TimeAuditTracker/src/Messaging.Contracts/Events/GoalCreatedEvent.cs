namespace Messaging.Contracts.Events;

public sealed record GoalCreatedEvent : IntegrationEvent
{
    public required Guid GoalId { get; init; }
    public required string Category { get; init; }
    public required int TargetMinutesPerDay { get; init; }
}
