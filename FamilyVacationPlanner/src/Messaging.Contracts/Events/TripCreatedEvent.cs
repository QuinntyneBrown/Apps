namespace Messaging.Contracts.Events;

public sealed record TripCreatedEvent : IntegrationEvent
{
    public required Guid TripId { get; init; }
    public required string Name { get; init; }
    public string? Destination { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}
