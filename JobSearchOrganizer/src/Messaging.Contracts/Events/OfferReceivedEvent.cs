namespace Messaging.Contracts.Events;

public sealed record OfferReceivedEvent : IntegrationEvent
{
    public required Guid OfferId { get; init; }
    public required Guid ApplicationId { get; init; }
    public required decimal SalaryAmount { get; init; }
}
