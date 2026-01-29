namespace Messaging.Contracts;

public class TripPlannedEvent : IntegrationEvent
{
    public Guid TripId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid? CampsiteId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}
