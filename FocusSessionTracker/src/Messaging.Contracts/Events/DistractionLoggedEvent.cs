namespace Messaging.Contracts.Events;

public sealed record DistractionLoggedEvent : IntegrationEvent
{
    public required Guid DistractionId { get; init; }
    public required Guid SessionId { get; init; }
    public required string Type { get; init; }
}
