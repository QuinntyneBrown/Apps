namespace Messaging.Contracts.Events;

public sealed record ScoreRecordedEvent : IntegrationEvent
{
    public required Guid ScoreId { get; init; }
    public required Guid RoundId { get; init; }
    public required int HoleNumber { get; init; }
    public required int Strokes { get; init; }
}
