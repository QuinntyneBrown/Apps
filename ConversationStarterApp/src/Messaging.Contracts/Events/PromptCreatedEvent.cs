namespace Messaging.Contracts.Events;

public sealed record PromptCreatedEvent : IntegrationEvent
{
    public required Guid PromptId { get; init; }
    public required string Text { get; init; }
    public required string Category { get; init; }
}
