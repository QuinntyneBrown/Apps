namespace Messaging.Contracts.Events;

public sealed record ItemUpdatedEvent : IntegrationEvent
{
    public required Guid ItemId { get; init; }
    public required string Name { get; init; }
}
