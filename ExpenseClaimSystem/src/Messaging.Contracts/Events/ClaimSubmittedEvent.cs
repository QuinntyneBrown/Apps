namespace Messaging.Contracts.Events;

public sealed record ClaimSubmittedEvent : IntegrationEvent
{
    public required Guid ClaimId { get; init; }
    public required decimal TotalAmount { get; init; }
    public required string Status { get; init; }
}
