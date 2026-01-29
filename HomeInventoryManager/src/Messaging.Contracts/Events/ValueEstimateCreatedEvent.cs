namespace Messaging.Contracts.Events;

public sealed record ValueEstimateCreatedEvent : IntegrationEvent
{
    public required Guid ValueEstimateId { get; init; }
    public required Guid ItemId { get; init; }
    public required decimal EstimatedValue { get; init; }
}
