namespace Messaging.Contracts.Events;

public sealed record ContributionRecordedEvent : IntegrationEvent
{
    public required Guid ContributionId { get; init; }
    public required Guid ScenarioId { get; init; }
    public required decimal Amount { get; init; }
    public required DateTime ContributionDate { get; init; }
}
