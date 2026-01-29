namespace Messaging.Contracts.Events;

public sealed record WatchlistItemAddedEvent : IntegrationEvent
{
    public required Guid WatchlistItemId { get; init; }
    public required string Title { get; init; }
    public required string ContentType { get; init; }
}
