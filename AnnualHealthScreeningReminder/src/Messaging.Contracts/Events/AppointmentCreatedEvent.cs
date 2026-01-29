namespace Messaging.Contracts.Events;

public sealed record AppointmentCreatedEvent : IntegrationEvent
{
    public required Guid AppointmentId { get; init; }
    public required DateTime AppointmentDate { get; init; }
    public required string ProviderName { get; init; }
    public required string AppointmentType { get; init; }
}
