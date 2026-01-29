namespace Messaging.Contracts.Events;

public sealed record AchievementCreatedEvent : IntegrationEvent
{
    public required Guid AchievementId { get; init; }
    public required string Title { get; init; }
    public required string AchievementType { get; init; }
    public required DateTime AchievedDate { get; init; }
}
