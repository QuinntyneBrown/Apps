namespace Messaging.Contracts.Events;

public sealed record HoldingAddedEvent : IntegrationEvent
{
    public required Guid HoldingId { get; init; }
    public required Guid AccountId { get; init; }
    public required string Symbol { get; init; }
    public required decimal Quantity { get; init; }
}
