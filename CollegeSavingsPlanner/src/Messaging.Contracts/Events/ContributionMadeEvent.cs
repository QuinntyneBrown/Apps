namespace Messaging.Contracts.Events;

public sealed record ContributionMadeEvent : IntegrationEvent
{
    public required Guid ContributionId { get; init; }
    public required Guid PlanId { get; init; }
    public required decimal Amount { get; init; }
}
