namespace Messaging.Contracts.Events;

public sealed record MessageSentEvent : IntegrationEvent
{
    public required Guid MessageId { get; init; }
    public required Guid RecipientId { get; init; }
    public required string Subject { get; init; }
}
