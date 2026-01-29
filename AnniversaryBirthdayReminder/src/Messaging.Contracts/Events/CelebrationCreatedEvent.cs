namespace Messaging.Contracts.Events;

public sealed record CelebrationCreatedEvent : IntegrationEvent
{
    public required Guid CelebrationId { get; init; }
    public required string Name { get; init; }
    public required DateTime Date { get; init; }
    public required string CelebrationType { get; init; }
}
