namespace Messaging.Contracts.Events;

public sealed record ExpenseRecordedEvent : IntegrationEvent
{
    public required Guid ExpenseId { get; init; }
    public required Guid BusinessId { get; init; }
    public required decimal Amount { get; init; }
}
