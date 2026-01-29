namespace Messaging.Contracts.Events;

public sealed record ComparisonCreatedEvent : IntegrationEvent
{
    public required Guid ComparisonId { get; init; }
    public required string ComparisonName { get; init; }
    public required int ProductCount { get; init; }
}
