namespace Messaging.Contracts.Events;

public sealed record MaintenancePerformedEvent : IntegrationEvent
{
    public required Guid MaintenanceId { get; init; }
    public required Guid EquipmentId { get; init; }
    public required DateTime PerformedDate { get; init; }
}
