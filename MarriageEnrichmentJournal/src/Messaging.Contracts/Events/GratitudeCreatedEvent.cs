namespace Messaging.Contracts.Events;

public sealed record GratitudeCreatedEvent : IntegrationEvent
{
    public required Guid GratitudeId { get; init; }
    public required string Content { get; init; }
    public required DateTime CreatedAt { get; init; }
}
