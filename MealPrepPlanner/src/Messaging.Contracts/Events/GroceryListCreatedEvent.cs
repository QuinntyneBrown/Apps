namespace Messaging.Contracts.Events;

public sealed record GroceryListCreatedEvent : IntegrationEvent
{
    public required Guid GroceryListId { get; init; }
    public required string Name { get; init; }
    public required DateTime CreatedAt { get; init; }
}
