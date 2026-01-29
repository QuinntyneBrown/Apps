namespace Messaging.Contracts;

public record ProjectCompletedEvent
{
    public Guid ProjectId { get; init; }
    public Guid TenantId { get; init; }
    public Guid UserId { get; init; }
    public Guid ClientId { get; init; }
    public DateTime CompletedAt { get; init; }
}
