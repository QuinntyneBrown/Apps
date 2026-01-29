namespace Messaging.Contracts.Events;

public sealed record BookingCreatedEvent : IntegrationEvent
{
    public required Guid BookingId { get; init; }
    public required Guid TripId { get; init; }
    public required string Type { get; init; }
    public string? ConfirmationNumber { get; init; }
}
