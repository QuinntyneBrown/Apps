namespace Messaging.Contracts.Events;

public sealed record TimeBlockStartedEvent : IntegrationEvent
{
    public required Guid TimeBlockId { get; init; }
    public required string Category { get; init; }
    public required DateTime StartTime { get; init; }
}
