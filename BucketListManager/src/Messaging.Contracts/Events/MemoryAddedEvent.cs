namespace Messaging.Contracts.Events;

public sealed record MemoryAddedEvent : IntegrationEvent
{
    public required Guid MemoryId { get; init; }
    public required Guid BucketListItemId { get; init; }
    public required string Title { get; init; }
}
