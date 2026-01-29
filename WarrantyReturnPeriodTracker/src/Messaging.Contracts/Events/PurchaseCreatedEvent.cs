namespace Messaging.Contracts.Events;

public sealed record PurchaseCreatedEvent : IntegrationEvent
{
    public required Guid PurchaseId { get; init; }
    public required string ProductName { get; init; }
    public required decimal Price { get; init; }
}
