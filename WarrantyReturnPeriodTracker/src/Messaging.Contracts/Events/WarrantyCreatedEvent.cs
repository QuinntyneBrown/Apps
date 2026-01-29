namespace Messaging.Contracts.Events;

public sealed record WarrantyCreatedEvent : IntegrationEvent
{
    public required Guid WarrantyId { get; init; }
    public required Guid PurchaseId { get; init; }
    public required DateTime ExpirationDate { get; init; }
}
