namespace Messaging.Contracts.Events;

public record MissionStatementCreatedEvent : IntegrationEvent
{
    public Guid MissionStatementId { get; init; }
    public required string Title { get; init; }
    public required string Statement { get; init; }
    public required string Version { get; init; }
}
