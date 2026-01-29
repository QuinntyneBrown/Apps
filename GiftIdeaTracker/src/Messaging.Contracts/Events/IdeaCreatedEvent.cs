namespace Messaging.Contracts.Events;

public sealed record IdeaCreatedEvent : IntegrationEvent
{
    public required Guid IdeaId { get; init; }
    public required string Title { get; init; }
    public required Guid RecipientId { get; init; }
}
