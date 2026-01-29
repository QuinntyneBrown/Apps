namespace Messaging.Contracts.Events;

public sealed record ReceiptUploadedEvent : IntegrationEvent
{
    public required Guid ReceiptId { get; init; }
    public required Guid DeductionId { get; init; }
    public required string FileName { get; init; }
}
