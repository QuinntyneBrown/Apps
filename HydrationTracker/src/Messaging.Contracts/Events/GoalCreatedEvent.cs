namespace Messaging.Contracts.Events;

public sealed record GoalCreatedEvent : IntegrationEvent
{
    public required Guid GoalId { get; init; }
    public required int DailyTargetMl { get; init; }
}
