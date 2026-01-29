namespace Messaging.Contracts.Events;

public abstract record IntegrationEvent
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
    public Guid? TenantId { get; init; }
    public Guid? UserId { get; init; }
}
