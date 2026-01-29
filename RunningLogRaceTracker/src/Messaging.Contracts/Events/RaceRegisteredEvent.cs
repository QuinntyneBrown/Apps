namespace Messaging.Contracts.Events;

public sealed record RaceRegisteredEvent : IntegrationEvent
{
    public required Guid RaceId { get; init; }
    public required string RaceName { get; init; }
    public required DateTime RaceDate { get; init; }
}
