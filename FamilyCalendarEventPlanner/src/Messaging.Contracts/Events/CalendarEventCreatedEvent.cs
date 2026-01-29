namespace Messaging.Contracts.Events;

public sealed record CalendarEventCreatedEvent : IntegrationEvent
{
    public required Guid CalendarEventId { get; init; }
    public required string Title { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
}
