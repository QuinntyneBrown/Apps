namespace Messaging.Contracts;

public record EventCreatedEvent : IntegrationEvent
{
    public Guid EventId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int EventType { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}
