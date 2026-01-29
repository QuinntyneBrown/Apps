namespace Messaging.Contracts.Events;

public sealed record RegistryCreatedEvent : IntegrationEvent
{
    public required Guid RegistryId { get; init; }
    public required string Name { get; init; }
    public required string EventType { get; init; }
}
