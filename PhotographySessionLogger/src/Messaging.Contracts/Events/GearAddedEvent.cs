namespace Messaging.Contracts.Events;

public sealed record GearAddedEvent : IntegrationEvent
{
    public required Guid GearId { get; init; }
    public required string Name { get; init; }
    public required string Type { get; init; }
}
