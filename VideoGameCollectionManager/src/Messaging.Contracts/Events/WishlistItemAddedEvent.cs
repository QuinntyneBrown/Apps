namespace Messaging.Contracts.Events;

public sealed record WishlistItemAddedEvent : IntegrationEvent
{
    public required Guid WishlistItemId { get; init; }
    public required string GameTitle { get; init; }
    public required string Platform { get; init; }
}
