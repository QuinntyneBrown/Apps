namespace Messaging.Contracts.Events;

public sealed record ProjectCreatedEvent : IntegrationEvent
{
    public required Guid ProjectId { get; init; }
    public required string Name { get; init; }
}
