namespace Messaging.Contracts.Events;

public sealed record WorkoutMappedEvent : IntegrationEvent
{
    public required Guid WorkoutMappingId { get; init; }
    public required Guid EquipmentId { get; init; }
    public required string WorkoutType { get; init; }
}
