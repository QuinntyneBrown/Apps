namespace Messaging.Contracts;

public record ContactCreatedEvent : IntegrationEvent
{
    public Guid ContactId { get; init; }
    public Guid EventId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Email { get; init; }
    public string? Company { get; init; }
}
