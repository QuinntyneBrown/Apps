namespace Messaging.Contracts.Events;

public sealed record TrendAnalyzedEvent : IntegrationEvent
{
    public required Guid TrendId { get; init; }
    public required string TrendType { get; init; }
    public required string Summary { get; init; }
}
