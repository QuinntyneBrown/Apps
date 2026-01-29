namespace Messaging.Contracts.Events;

public sealed record PurchaseCompletedEvent : IntegrationEvent
{
    public required Guid PurchaseId { get; init; }
    public required Guid IdeaId { get; init; }
    public required decimal Amount { get; init; }
}
