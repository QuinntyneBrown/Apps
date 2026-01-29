namespace Messaging.Contracts.Events;

public sealed record PageCreatedEvent : IntegrationEvent
{
    public required Guid PageId { get; init; }
    public required string Title { get; init; }
    public required string Slug { get; init; }
}
