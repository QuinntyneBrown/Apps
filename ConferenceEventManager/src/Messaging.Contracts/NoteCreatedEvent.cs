namespace Messaging.Contracts;

public record NoteCreatedEvent : IntegrationEvent
{
    public Guid NoteId { get; init; }
    public Guid EventId { get; init; }
    public string Content { get; init; } = string.Empty;
    public string? Category { get; init; }
}
