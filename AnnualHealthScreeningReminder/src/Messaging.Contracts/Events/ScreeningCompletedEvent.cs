namespace Messaging.Contracts.Events;

public sealed record ScreeningCompletedEvent : IntegrationEvent
{
    public required Guid ScreeningId { get; init; }
    public required string ScreeningType { get; init; }
    public required DateTime CompletedDate { get; init; }
    public required string Result { get; init; }
}
