namespace Messaging.Contracts.Events;

public sealed record NeighborConnectedEvent : IntegrationEvent
{
    public required Guid NeighborId { get; init; }
    public required Guid ConnectedUserId { get; init; }
}
