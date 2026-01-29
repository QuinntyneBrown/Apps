namespace Messaging.Contracts.Events;

public sealed record SessionAnalyticsUpdatedEvent : IntegrationEvent
{
    public required Guid AnalyticsId { get; init; }
    public required int TotalSessions { get; init; }
    public required int TotalFocusMinutes { get; init; }
}
