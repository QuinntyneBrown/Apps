namespace Messaging.Contracts.Events;

public sealed record ActivityCreatedEvent : IntegrationEvent
{
    public required Guid ActivityId { get; init; }
    public required string Name { get; init; }
    public required string ActivityType { get; init; }
}
