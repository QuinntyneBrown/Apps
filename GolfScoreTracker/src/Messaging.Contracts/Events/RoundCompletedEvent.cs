namespace Messaging.Contracts.Events;

public sealed record RoundCompletedEvent : IntegrationEvent
{
    public required Guid RoundId { get; init; }
    public required Guid CourseId { get; init; }
    public required int TotalScore { get; init; }
}
