namespace Messaging.Contracts.Events;

public sealed record ApplianceCreatedEvent : IntegrationEvent
{
    public required Guid ApplianceId { get; init; }
    public required string Name { get; init; }
    public required string ApplianceType { get; init; }
}
