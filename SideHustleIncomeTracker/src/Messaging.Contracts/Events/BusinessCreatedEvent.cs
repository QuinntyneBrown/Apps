namespace Messaging.Contracts.Events;

public sealed record BusinessCreatedEvent : IntegrationEvent
{
    public required Guid BusinessId { get; init; }
    public required string Name { get; init; }
}
