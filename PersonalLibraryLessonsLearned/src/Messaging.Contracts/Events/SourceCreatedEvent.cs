namespace Messaging.Contracts.Events;

public sealed record SourceCreatedEvent : IntegrationEvent
{
    public required Guid SourceId { get; init; }
    public required string Title { get; init; }
    public required string SourceType { get; init; }
}
