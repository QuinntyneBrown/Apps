namespace Messaging.Contracts;

public record ClientDeactivatedEvent
{
    public Guid ClientId { get; init; }
    public Guid TenantId { get; init; }
    public Guid UserId { get; init; }
    public DateTime DeactivatedAt { get; init; }
}
