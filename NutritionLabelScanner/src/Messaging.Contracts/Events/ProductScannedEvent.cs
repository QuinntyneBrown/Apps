namespace Messaging.Contracts.Events;

public sealed record ProductScannedEvent : IntegrationEvent
{
    public required Guid ProductId { get; init; }
    public required string Barcode { get; init; }
    public required string ProductName { get; init; }
}
