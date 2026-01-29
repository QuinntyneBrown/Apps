namespace Messaging.Contracts.Events;

public sealed record DeadlineCreatedEvent : IntegrationEvent
{
    public required Guid DeadlineId { get; init; }
    public required string Title { get; init; }
    public required DateTime DeadlineDateTime { get; init; }
}
