namespace Messaging.Contracts;

public record ClientCreatedEvent
{
    public Guid ClientId { get; init; }
    public Guid TenantId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? CompanyName { get; init; }
    public string? Email { get; init; }
    public DateTime CreatedAt { get; init; }
}
