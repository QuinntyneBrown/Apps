namespace Messaging.Contracts.Events;

public sealed record ReviewCreatedEvent : IntegrationEvent
{
    public required Guid ReviewId { get; init; }
    public required Guid ContentId { get; init; }
    public required string ReviewText { get; init; }
}
