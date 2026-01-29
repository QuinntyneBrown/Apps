namespace Messaging.Contracts.Events;

public sealed record PlaySessionLoggedEvent : IntegrationEvent
{
    public required Guid SessionId { get; init; }
    public required Guid GameId { get; init; }
    public required int DurationMinutes { get; init; }
}
