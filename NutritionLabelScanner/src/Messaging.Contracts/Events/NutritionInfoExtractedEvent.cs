namespace Messaging.Contracts.Events;

public sealed record NutritionInfoExtractedEvent : IntegrationEvent
{
    public required Guid NutritionInfoId { get; init; }
    public required Guid ProductId { get; init; }
    public required int Calories { get; init; }
}
