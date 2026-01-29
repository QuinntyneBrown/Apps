namespace Messaging.Contracts.Events;

public sealed record AdminTaskCreatedEvent : IntegrationEvent
{
    public required Guid AdminTaskId { get; init; }
    public required string Title { get; init; }
    public required string Category { get; init; }
}
