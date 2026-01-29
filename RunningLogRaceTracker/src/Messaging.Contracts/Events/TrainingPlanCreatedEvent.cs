namespace Messaging.Contracts.Events;

public sealed record TrainingPlanCreatedEvent : IntegrationEvent
{
    public required Guid TrainingPlanId { get; init; }
    public required string PlanName { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
}
