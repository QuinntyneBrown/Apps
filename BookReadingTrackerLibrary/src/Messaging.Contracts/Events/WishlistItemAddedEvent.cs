namespace Messaging.Contracts.Events;

public sealed record WishlistItemAddedEvent : IntegrationEvent
{
    public required Guid WishlistItemId { get; init; }
    public required string BookTitle { get; init; }
    public required string Author { get; init; }
}
