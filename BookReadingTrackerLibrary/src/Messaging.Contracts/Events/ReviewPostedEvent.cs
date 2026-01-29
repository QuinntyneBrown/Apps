namespace Messaging.Contracts.Events;

public sealed record ReviewPostedEvent : IntegrationEvent
{
    public required Guid ReviewId { get; init; }
    public required Guid BookId { get; init; }
    public required int Rating { get; init; }
}
