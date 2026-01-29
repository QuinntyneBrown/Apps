namespace Messaging.Contracts.Events;

public record ValueCreatedEvent : IntegrationEvent
{
    public Guid ValueId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public int Priority { get; init; }
}
