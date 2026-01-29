namespace Messaging.Contracts.Events;

public sealed record TaskCreatedEvent : IntegrationEvent
{
    public required Guid TaskId { get; init; }
    public required string Title { get; init; }
    public required string Urgency { get; init; }
    public required string Importance { get; init; }
}
