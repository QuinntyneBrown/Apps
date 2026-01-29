namespace Messaging.Contracts.Events;

public sealed record LetterCreatedEvent : IntegrationEvent
{
    public required Guid LetterId { get; init; }
    public required string Subject { get; init; }
    public required DateTime ScheduledDeliveryDate { get; init; }
}
