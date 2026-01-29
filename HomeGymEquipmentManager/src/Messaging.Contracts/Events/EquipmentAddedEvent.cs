namespace Messaging.Contracts.Events;

public sealed record EquipmentAddedEvent : IntegrationEvent
{
    public required Guid EquipmentId { get; init; }
    public required string Name { get; init; }
    public required string EquipmentType { get; init; }
}
