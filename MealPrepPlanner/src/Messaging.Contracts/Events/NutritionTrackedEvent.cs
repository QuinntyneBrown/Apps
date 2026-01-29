namespace Messaging.Contracts.Events;

public sealed record NutritionTrackedEvent : IntegrationEvent
{
    public required Guid NutritionId { get; init; }
    public required int Calories { get; init; }
    public required DateTime TrackedAt { get; init; }
}
