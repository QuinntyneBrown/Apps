namespace Messaging.Contracts.Events;

public sealed record InterviewScheduledEvent : IntegrationEvent
{
    public required Guid InterviewId { get; init; }
    public required Guid ApplicationId { get; init; }
    public required DateTime ScheduledDate { get; init; }
}
