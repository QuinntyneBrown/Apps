namespace Messaging.Contracts.Events;

public sealed record MilestoneAchievedEvent : IntegrationEvent
{
    public required Guid MilestoneId { get; init; }
    public required string Name { get; init; }
    public required DateTime AchievedAt { get; init; }
}
