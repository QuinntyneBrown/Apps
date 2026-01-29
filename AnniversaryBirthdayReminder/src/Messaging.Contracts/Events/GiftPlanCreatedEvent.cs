namespace Messaging.Contracts.Events;

public sealed record GiftPlanCreatedEvent : IntegrationEvent
{
    public required Guid GiftPlanId { get; init; }
    public required Guid CelebrationId { get; init; }
    public required string GiftIdea { get; init; }
    public required decimal Budget { get; init; }
}
