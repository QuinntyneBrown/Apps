namespace Messaging.Contracts.Events;

public sealed record ReminderTriggeredEvent : IntegrationEvent
{
    public required Guid ReminderId { get; init; }
    public required TimeSpan ScheduledTime { get; init; }
    public required string Message { get; init; }
}
