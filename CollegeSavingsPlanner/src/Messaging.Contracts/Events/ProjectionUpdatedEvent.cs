namespace Messaging.Contracts.Events;

public sealed record ProjectionUpdatedEvent : IntegrationEvent
{
    public required Guid ProjectionId { get; init; }
    public required Guid PlanId { get; init; }
    public required decimal ProjectedAmount { get; init; }
}
