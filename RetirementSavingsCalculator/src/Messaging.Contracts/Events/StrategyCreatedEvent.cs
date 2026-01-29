namespace Messaging.Contracts.Events;

public sealed record StrategyCreatedEvent : IntegrationEvent
{
    public required Guid StrategyId { get; init; }
    public required Guid ScenarioId { get; init; }
    public required string Name { get; init; }
    public required decimal WithdrawalRate { get; init; }
}
