namespace Messaging.Contracts;

public record UserCreatedEvent
{
    public Guid UserId { get; init; }
    public Guid TenantId { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
