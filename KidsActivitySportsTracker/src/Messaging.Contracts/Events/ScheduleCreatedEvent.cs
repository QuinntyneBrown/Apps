namespace Messaging.Contracts.Events;

public sealed record ScheduleCreatedEvent : IntegrationEvent
{
    public required Guid ScheduleId { get; init; }
    public required Guid ActivityId { get; init; }
    public required DateTime EventDate { get; init; }
}
