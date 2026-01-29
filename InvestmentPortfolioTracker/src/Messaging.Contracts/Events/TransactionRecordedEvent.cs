namespace Messaging.Contracts.Events;

public sealed record TransactionRecordedEvent : IntegrationEvent
{
    public required Guid TransactionId { get; init; }
    public required Guid AccountId { get; init; }
    public required string TransactionType { get; init; }
    public required decimal Amount { get; init; }
}
