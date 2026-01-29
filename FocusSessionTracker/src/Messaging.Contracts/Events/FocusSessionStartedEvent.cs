namespace Messaging.Contracts.Events;

public sealed record FocusSessionStartedEvent : IntegrationEvent
{
    public required Guid SessionId { get; init; }
    public required DateTime StartTime { get; init; }
    public required int PlannedDurationMinutes { get; init; }
}
