namespace Messaging.Contracts.Events;

public sealed record ContributionAddedEvent : IntegrationEvent
{
    public required Guid ContributionId { get; init; }
    public required Guid GoalId { get; init; }
    public required decimal Amount { get; init; }
}
