namespace Messaging.Contracts.Events;

public sealed record HandicapUpdatedEvent : IntegrationEvent
{
    public required decimal NewHandicap { get; init; }
    public required decimal PreviousHandicap { get; init; }
}
