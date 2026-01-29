namespace Messaging.Contracts;

public abstract class IntegrationEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public Guid TenantId { get; init; }
    public Guid UserId { get; init; }
}
