namespace Messaging.Contracts.Events;

public sealed record InjuryRecordedEvent : IntegrationEvent
{
    public required Guid InjuryId { get; init; }
    public required string InjuryType { get; init; }
    public required string Severity { get; init; }
}
