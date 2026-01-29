namespace Messaging.Contracts.Events;

public sealed record ReminderScheduledEvent : IntegrationEvent
{
    public required Guid ReminderId { get; init; }
    public required Guid LessonId { get; init; }
    public required DateTime ScheduledDate { get; init; }
}
