namespace Messaging.Contracts.Events;

public sealed record FavoriteCreatedEvent : IntegrationEvent
{
    public required Guid FavoriteId { get; init; }
    public required Guid PromptId { get; init; }
}
