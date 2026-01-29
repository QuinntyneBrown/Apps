namespace Messaging.Contracts.Events;

public sealed record TaxReportGeneratedEvent : IntegrationEvent
{
    public required Guid TaxReportId { get; init; }
    public required int TaxYear { get; init; }
}
