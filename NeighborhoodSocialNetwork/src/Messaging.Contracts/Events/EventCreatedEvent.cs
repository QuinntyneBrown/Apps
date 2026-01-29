namespace Messaging.Contracts.Events;

public sealed record EventCreatedEvent : IntegrationEvent
{
    public required Guid EventId { get; init; }
    public required string Title { get; init; }
    public required DateTime EventDate { get; init; }
}
