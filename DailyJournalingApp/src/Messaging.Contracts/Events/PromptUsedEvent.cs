namespace Messaging.Contracts.Events;

public sealed record PromptUsedEvent : IntegrationEvent
{
    public required Guid PromptId { get; init; }
    public required Guid JournalEntryId { get; init; }
}
