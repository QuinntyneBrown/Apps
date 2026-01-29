namespace Messaging.Contracts.Events;

public sealed record BucketListItemCreatedEvent : IntegrationEvent
{
    public required Guid BucketListItemId { get; init; }
    public required string Title { get; init; }
    public required string Category { get; init; }
}
