namespace Messaging.Contracts.Events;

public sealed record RecipeCreatedEvent : IntegrationEvent
{
    public required Guid RecipeId { get; init; }
    public required string Name { get; init; }
    public required int PrepTimeMinutes { get; init; }
}
