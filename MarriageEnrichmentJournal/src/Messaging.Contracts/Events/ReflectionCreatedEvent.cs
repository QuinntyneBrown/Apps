namespace Messaging.Contracts.Events;

public sealed record ReflectionCreatedEvent : IntegrationEvent
{
    public required Guid ReflectionId { get; init; }
    public required string Topic { get; init; }
    public required DateTime CreatedAt { get; init; }
}
