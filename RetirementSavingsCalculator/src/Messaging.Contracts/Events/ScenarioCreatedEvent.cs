namespace Messaging.Contracts.Events;

public sealed record ScenarioCreatedEvent : IntegrationEvent
{
    public required Guid ScenarioId { get; init; }
    public required string Name { get; init; }
    public required int CurrentAge { get; init; }
    public required int RetirementAge { get; init; }
}
