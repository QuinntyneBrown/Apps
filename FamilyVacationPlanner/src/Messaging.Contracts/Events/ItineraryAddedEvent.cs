namespace Messaging.Contracts.Events;

public sealed record ItineraryAddedEvent : IntegrationEvent
{
    public required Guid ItineraryId { get; init; }
    public required Guid TripId { get; init; }
    public required DateTime Date { get; init; }
    public string? Activity { get; init; }
}
