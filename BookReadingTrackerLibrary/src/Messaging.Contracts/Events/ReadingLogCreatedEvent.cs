namespace Messaging.Contracts.Events;

public sealed record ReadingLogCreatedEvent : IntegrationEvent
{
    public required Guid ReadingLogId { get; init; }
    public required Guid BookId { get; init; }
    public required int PagesRead { get; init; }
}
