namespace Messaging.Contracts.Events;

public sealed record ServiceRecordCreatedEvent : IntegrationEvent
{
    public required Guid ServiceRecordId { get; init; }
    public required Guid ApplianceId { get; init; }
    public required DateTime ServiceDate { get; init; }
}
