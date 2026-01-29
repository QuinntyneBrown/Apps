namespace Messaging.Contracts;

public class CampsiteAddedEvent : IntegrationEvent
{
    public Guid CampsiteId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
}
