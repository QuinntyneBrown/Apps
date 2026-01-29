namespace Messaging.Contracts.Events;

public sealed record ExpenseRecordedEvent : IntegrationEvent
{
    public required Guid ExpenseId { get; init; }
    public required decimal Amount { get; init; }
    public required string Category { get; init; }
}
