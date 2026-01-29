namespace Messaging.Contracts.Events;

public sealed record TimeBlockEndedEvent : IntegrationEvent
{
    public required Guid TimeBlockId { get; init; }
    public required DateTime EndTime { get; init; }
    public required double DurationMinutes { get; init; }
}
