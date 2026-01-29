namespace Messaging.Contracts.Events;

public sealed record PagePublishedEvent : IntegrationEvent
{
    public required Guid PageId { get; init; }
    public required string Title { get; init; }
}
