namespace Messaging.Contracts.Events;

public sealed record MilestoneAchievedEvent : IntegrationEvent
{
    public required Guid MilestoneId { get; init; }
    public required Guid InjuryId { get; init; }
    public required string Description { get; init; }
}
