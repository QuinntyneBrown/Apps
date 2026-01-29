namespace Messaging.Contracts.Events;

public sealed record ReadingRecordedEvent : IntegrationEvent
{
    public required Guid ReadingId { get; init; }
    public required int Systolic { get; init; }
    public required int Diastolic { get; init; }
    public required int Pulse { get; init; }
    public required DateTime RecordedAt { get; init; }
}
