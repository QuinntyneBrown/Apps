namespace Messaging.Contracts.Events;

public sealed record DividendReceivedEvent : IntegrationEvent
{
    public required Guid DividendId { get; init; }
    public required Guid HoldingId { get; init; }
    public required decimal Amount { get; init; }
}
