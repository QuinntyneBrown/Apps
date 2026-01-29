namespace Messaging.Contracts.Events;

public sealed record DeliveryScheduleCreatedEvent : IntegrationEvent
{
    public required Guid DeliveryScheduleId { get; init; }
    public required Guid LetterId { get; init; }
    public required DateTime ScheduledDateTime { get; init; }
    public required string DeliveryMethod { get; init; }
}
