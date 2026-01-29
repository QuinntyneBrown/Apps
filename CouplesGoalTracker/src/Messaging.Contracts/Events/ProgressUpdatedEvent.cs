namespace Messaging.Contracts.Events;

public sealed record ProgressUpdatedEvent : IntegrationEvent
{
    public required Guid ProgressId { get; init; }
    public required Guid GoalId { get; init; }
    public required decimal PercentageComplete { get; init; }
}
