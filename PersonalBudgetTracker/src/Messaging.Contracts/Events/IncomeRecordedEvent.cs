namespace Messaging.Contracts.Events;

public sealed record IncomeRecordedEvent : IntegrationEvent
{
    public required Guid IncomeId { get; init; }
    public required decimal Amount { get; init; }
    public required string Source { get; init; }
}
