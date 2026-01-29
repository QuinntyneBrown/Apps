namespace Messaging.Contracts.Events;

public sealed record ChoreCreatedEvent : IntegrationEvent
{
    public required Guid ChoreId { get; init; }
    public required string Name { get; init; }
}
