namespace Messaging.Contracts.Events;

public sealed record OfferReceivedEvent : IntegrationEvent
{
    public required Guid OfferId { get; init; }
    public required Guid LoanId { get; init; }
    public required string LenderName { get; init; }
    public required decimal InterestRate { get; init; }
}
