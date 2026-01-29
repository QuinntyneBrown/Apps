namespace Messaging.Contracts.Events;

public sealed record MealPlanCreatedEvent : IntegrationEvent
{
    public required Guid MealPlanId { get; init; }
    public required string Name { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
}
