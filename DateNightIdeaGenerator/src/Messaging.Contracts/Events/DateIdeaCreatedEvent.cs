namespace Messaging.Contracts.Events;

public sealed record DateIdeaCreatedEvent : IntegrationEvent
{
    public required Guid DateIdeaId { get; init; }
    public required string Title { get; init; }
    public required string Category { get; init; }
}
