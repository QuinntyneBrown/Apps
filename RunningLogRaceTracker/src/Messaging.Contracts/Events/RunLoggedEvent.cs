namespace Messaging.Contracts.Events;

public sealed record RunLoggedEvent : IntegrationEvent
{
    public required Guid RunId { get; init; }
    public required decimal Distance { get; init; }
    public required int DurationMinutes { get; init; }
}
