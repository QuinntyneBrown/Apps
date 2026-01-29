namespace Messaging.Contracts.Events;

public sealed record BudgetCreatedEvent : IntegrationEvent
{
    public required Guid BudgetId { get; init; }
    public required string Name { get; init; }
    public required decimal AllocatedAmount { get; init; }
}
