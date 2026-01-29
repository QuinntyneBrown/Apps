namespace Messaging.Contracts;

public record SessionCreatedEvent : IntegrationEvent
{
    public Guid SessionId { get; init; }
    public Guid EventId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Speaker { get; init; }
    public DateTime StartTime { get; init; }
}
