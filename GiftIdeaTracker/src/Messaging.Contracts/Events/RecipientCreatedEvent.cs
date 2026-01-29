namespace Messaging.Contracts.Events;

public sealed record RecipientCreatedEvent : IntegrationEvent
{
    public required Guid RecipientId { get; init; }
    public required string Name { get; init; }
}
