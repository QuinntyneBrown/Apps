namespace Messaging.Contracts.Events;

public sealed record RegistryItemAddedEvent : IntegrationEvent
{
    public required Guid RegistryItemId { get; init; }
    public required Guid RegistryId { get; init; }
    public required string Name { get; init; }
}
