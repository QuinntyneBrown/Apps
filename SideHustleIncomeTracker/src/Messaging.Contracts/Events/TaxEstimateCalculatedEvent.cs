namespace Messaging.Contracts.Events;

public sealed record TaxEstimateCalculatedEvent : IntegrationEvent
{
    public required Guid TaxEstimateId { get; init; }
    public required decimal EstimatedTax { get; init; }
    public required int Year { get; init; }
}
