namespace Messaging.Contracts.Events;

public sealed record PlanCreatedEvent : IntegrationEvent
{
    public required Guid PlanId { get; init; }
    public required string Name { get; init; }
    public required decimal TargetAmount { get; init; }
}
