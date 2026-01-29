namespace Messaging.Contracts.Events;

public sealed record PolicyCreatedEvent : IntegrationEvent
{
    public required Guid PolicyId { get; init; }
    public required Guid VehicleId { get; init; }
    public required string Provider { get; init; }
    public required string PolicyNumber { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
}
