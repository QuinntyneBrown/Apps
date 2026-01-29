namespace Messaging.Contracts.Events;

public sealed record CarpoolCreatedEvent : IntegrationEvent
{
    public required Guid CarpoolId { get; init; }
    public required Guid ScheduleId { get; init; }
    public required string DriverName { get; init; }
}
