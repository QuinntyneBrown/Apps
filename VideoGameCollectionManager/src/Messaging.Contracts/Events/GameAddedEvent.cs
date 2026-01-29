namespace Messaging.Contracts.Events;

public sealed record GameAddedEvent : IntegrationEvent
{
    public required Guid GameId { get; init; }
    public required string Title { get; init; }
    public required string Platform { get; init; }
}
