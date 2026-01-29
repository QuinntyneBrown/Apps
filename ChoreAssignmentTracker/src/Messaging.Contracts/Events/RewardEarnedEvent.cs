namespace Messaging.Contracts.Events;

public sealed record RewardEarnedEvent : IntegrationEvent
{
    public required Guid RewardId { get; init; }
    public required Guid FamilyMemberId { get; init; }
    public required int Points { get; init; }
}
