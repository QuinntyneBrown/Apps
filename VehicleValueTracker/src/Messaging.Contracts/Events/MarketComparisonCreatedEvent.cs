namespace Messaging.Contracts.Events;

public sealed record MarketComparisonCreatedEvent : IntegrationEvent
{
    public required Guid ComparisonId { get; init; }
    public required Guid VehicleId { get; init; }
    public required decimal MarketPrice { get; init; }
}
