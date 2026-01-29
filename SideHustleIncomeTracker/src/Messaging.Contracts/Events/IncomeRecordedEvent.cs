namespace Messaging.Contracts.Events;

public sealed record IncomeRecordedEvent : IntegrationEvent
{
    public required Guid IncomeId { get; init; }
    public required Guid BusinessId { get; init; }
    public required decimal Amount { get; init; }
}
