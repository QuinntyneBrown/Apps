namespace Messaging.Contracts.Events;

public sealed record TaskCompletedEvent : IntegrationEvent
{
    public required Guid TaskId { get; init; }
    public required DateTime CompletedAt { get; init; }
}
