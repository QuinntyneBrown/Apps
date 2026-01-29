namespace Messaging.Contracts.Events;

public sealed record CategoryCreatedEvent : IntegrationEvent
{
    public required Guid CategoryId { get; init; }
    public required string Name { get; init; }
}
