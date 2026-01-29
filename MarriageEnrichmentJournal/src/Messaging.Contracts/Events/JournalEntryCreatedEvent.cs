namespace Messaging.Contracts.Events;

public sealed record JournalEntryCreatedEvent : IntegrationEvent
{
    public required Guid JournalEntryId { get; init; }
    public required string Title { get; init; }
    public required DateTime EntryDate { get; init; }
}
