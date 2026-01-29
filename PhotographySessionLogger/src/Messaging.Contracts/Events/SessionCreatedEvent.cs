namespace Messaging.Contracts.Events;

public sealed record SessionCreatedEvent : IntegrationEvent
{
    public required Guid SessionId { get; init; }
    public required string Location { get; init; }
    public required DateTime SessionDate { get; init; }
}
