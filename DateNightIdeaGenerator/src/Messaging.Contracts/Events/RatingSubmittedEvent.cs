namespace Messaging.Contracts.Events;

public sealed record RatingSubmittedEvent : IntegrationEvent
{
    public required Guid RatingId { get; init; }
    public required Guid ExperienceId { get; init; }
    public required int Score { get; init; }
}
