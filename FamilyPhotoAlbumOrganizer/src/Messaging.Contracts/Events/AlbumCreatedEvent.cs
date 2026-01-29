namespace Messaging.Contracts.Events;

public sealed record AlbumCreatedEvent : IntegrationEvent
{
    public required Guid AlbumId { get; init; }
    public required string Name { get; init; }
}
