namespace Messaging.Contracts.Events;

public sealed record LetterDeliveredEvent : IntegrationEvent
{
    public required Guid LetterId { get; init; }
    public required DateTime DeliveredAt { get; init; }
}
