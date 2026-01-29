namespace Messaging.Contracts.Events;

public sealed record BudgetCreatedEvent : IntegrationEvent
{
    public required Guid VacationBudgetId { get; init; }
    public required Guid TripId { get; init; }
    public required string Category { get; init; }
    public required decimal AllocatedAmount { get; init; }
}
