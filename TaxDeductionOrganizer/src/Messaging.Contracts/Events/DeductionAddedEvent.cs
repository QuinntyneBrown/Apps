namespace Messaging.Contracts.Events;

public sealed record DeductionAddedEvent : IntegrationEvent
{
    public required Guid DeductionId { get; init; }
    public required Guid TaxYearId { get; init; }
    public required decimal Amount { get; init; }
    public required string Category { get; init; }
}
