namespace Messaging.Contracts;

public class ReviewPostedEvent : IntegrationEvent
{
    public Guid ReviewId { get; init; }
    public Guid? CampsiteId { get; init; }
    public int Rating { get; init; }
    public string Content { get; init; } = string.Empty;
}
