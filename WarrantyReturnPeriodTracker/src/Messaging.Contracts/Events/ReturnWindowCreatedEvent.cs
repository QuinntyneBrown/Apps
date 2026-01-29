namespace Messaging.Contracts.Events;

public sealed record ReturnWindowCreatedEvent : IntegrationEvent
{
    public required Guid ReturnWindowId { get; init; }
    public required Guid PurchaseId { get; init; }
    public required DateTime ExpirationDate { get; init; }
}
