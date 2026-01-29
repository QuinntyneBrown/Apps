namespace Messaging.Contracts;

public class GearChecklistCreatedEvent : IntegrationEvent
{
    public Guid GearChecklistId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid? TripId { get; init; }
}
