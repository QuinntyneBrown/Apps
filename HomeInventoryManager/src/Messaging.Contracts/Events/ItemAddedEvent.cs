namespace Messaging.Contracts.Events;

public sealed record ItemAddedEvent : IntegrationEvent
{
    public required Guid ItemId { get; init; }
    public required string Name { get; init; }
    public required string Category { get; init; }
    public required string Room { get; init; }
}
