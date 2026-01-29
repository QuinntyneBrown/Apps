namespace Messaging.Contracts.Events;

public sealed record BookAddedEvent : IntegrationEvent
{
    public required Guid BookId { get; init; }
    public required string Title { get; init; }
    public required string Author { get; init; }
}
