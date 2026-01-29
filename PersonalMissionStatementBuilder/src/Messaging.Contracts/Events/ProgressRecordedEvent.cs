namespace Messaging.Contracts.Events;

public record ProgressRecordedEvent : IntegrationEvent
{
    public Guid ProgressId { get; init; }
    public Guid GoalId { get; init; }
    public decimal PercentComplete { get; init; }
    public required string Notes { get; init; }
    public DateTime RecordedAt { get; init; }
}
