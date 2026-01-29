namespace Messaging.Contracts;

public record ProjectCreatedEvent
{
    public Guid ProjectId { get; init; }
    public Guid TenantId { get; init; }
    public Guid UserId { get; init; }
    public Guid ClientId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime? DueDate { get; init; }
    public DateTime CreatedAt { get; init; }
}
