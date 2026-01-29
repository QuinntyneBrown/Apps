namespace Messaging.Contracts.Events;

public sealed record GroupCreatedEvent : IntegrationEvent
{
    public required Guid GroupId { get; init; }
    public required string Name { get; init; }
}
