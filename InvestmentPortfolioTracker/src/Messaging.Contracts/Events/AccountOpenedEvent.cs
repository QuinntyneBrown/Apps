namespace Messaging.Contracts.Events;

public sealed record AccountOpenedEvent : IntegrationEvent
{
    public required Guid AccountId { get; init; }
    public required string AccountName { get; init; }
    public required string AccountType { get; init; }
}
