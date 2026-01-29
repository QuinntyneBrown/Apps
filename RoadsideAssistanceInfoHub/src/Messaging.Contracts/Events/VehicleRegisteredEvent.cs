namespace Messaging.Contracts.Events;

public sealed record VehicleRegisteredEvent : IntegrationEvent
{
    public required Guid VehicleId { get; init; }
    public required string Make { get; init; }
    public required string Model { get; init; }
    public required int Year { get; init; }
}
