namespace Messaging.Contracts.Events;

public sealed record ExperienceRecordedEvent : IntegrationEvent
{
    public required Guid ExperienceId { get; init; }
    public required Guid DateIdeaId { get; init; }
    public required DateTime ExperienceDate { get; init; }
}
