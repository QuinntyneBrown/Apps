namespace Messaging.Contracts.Events;

public sealed record ExpenseCreatedEvent : IntegrationEvent
{
    public required Guid ExpenseId { get; init; }
    public required string Description { get; init; }
    public required decimal Amount { get; init; }
}
