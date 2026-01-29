namespace Messaging.Contracts.Events;

public sealed record RevisionCreatedEvent : IntegrationEvent
{
    public required Guid RevisionId { get; init; }
    public required Guid PageId { get; init; }
    public required int VersionNumber { get; init; }
}
