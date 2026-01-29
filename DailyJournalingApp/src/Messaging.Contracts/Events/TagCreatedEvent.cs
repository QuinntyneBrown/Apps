namespace Messaging.Contracts.Events;

public sealed record TagCreatedEvent : IntegrationEvent
{
    public required Guid TagId { get; init; }
    public required string Name { get; init; }
}
