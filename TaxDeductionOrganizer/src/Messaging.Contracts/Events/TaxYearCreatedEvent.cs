namespace Messaging.Contracts.Events;

public sealed record TaxYearCreatedEvent : IntegrationEvent
{
    public required Guid TaxYearId { get; init; }
    public required int Year { get; init; }
}
