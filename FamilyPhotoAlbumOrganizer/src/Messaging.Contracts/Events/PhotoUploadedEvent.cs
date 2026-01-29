namespace Messaging.Contracts.Events;

public sealed record PhotoUploadedEvent : IntegrationEvent
{
    public required Guid PhotoId { get; init; }
    public required Guid AlbumId { get; init; }
    public required string FileName { get; init; }
}
