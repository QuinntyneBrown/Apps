namespace Messaging.Contracts.Events;

public sealed record ManualUploadedEvent : IntegrationEvent
{
    public required Guid ManualId { get; init; }
    public required Guid ApplianceId { get; init; }
    public required string Title { get; init; }
}
