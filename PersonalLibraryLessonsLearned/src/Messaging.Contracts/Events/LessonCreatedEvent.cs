namespace Messaging.Contracts.Events;

public sealed record LessonCreatedEvent : IntegrationEvent
{
    public required Guid LessonId { get; init; }
    public required string Title { get; init; }
    public required string Category { get; init; }
    public Guid? SourceId { get; init; }
}
