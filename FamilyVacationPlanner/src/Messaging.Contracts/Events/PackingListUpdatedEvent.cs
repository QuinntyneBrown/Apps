namespace Messaging.Contracts.Events;

public sealed record PackingListUpdatedEvent : IntegrationEvent
{
    public required Guid PackingListId { get; init; }
    public required Guid TripId { get; init; }
    public required string ItemName { get; init; }
    public required bool IsPacked { get; init; }
}
