namespace Messaging.Contracts.Events;

public sealed record AuditReportGeneratedEvent : IntegrationEvent
{
    public required Guid ReportId { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
}
