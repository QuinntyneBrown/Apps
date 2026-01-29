namespace Identity.Api.Features;

public record UserDto
{
    public Guid UserId { get; init; }
    public Guid TenantId { get; init; }
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
    public List<string> Roles { get; init; } = new();
}
