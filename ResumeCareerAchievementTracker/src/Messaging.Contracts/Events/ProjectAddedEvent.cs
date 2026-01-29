namespace Messaging.Contracts.Events;

public sealed record ProjectAddedEvent : IntegrationEvent
{
    public required Guid ProjectId { get; init; }
    public required string Name { get; init; }
    public required DateTime StartDate { get; init; }
}
