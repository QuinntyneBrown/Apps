namespace Messaging.Contracts.Events;

public sealed record WarrantyCreatedEvent : IntegrationEvent
{
    public required Guid WarrantyId { get; init; }
    public required Guid ApplianceId { get; init; }
    public DateTime? EndDate { get; init; }
}
