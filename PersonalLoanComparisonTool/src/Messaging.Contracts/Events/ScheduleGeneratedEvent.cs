namespace Messaging.Contracts.Events;

public sealed record ScheduleGeneratedEvent : IntegrationEvent
{
    public required Guid ScheduleId { get; init; }
    public required Guid OfferId { get; init; }
    public required int TotalPayments { get; init; }
}
