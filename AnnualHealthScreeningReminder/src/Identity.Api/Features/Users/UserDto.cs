namespace Identity.Api.Features.Users;

public record UserDto
{
    public Guid UserId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public List<RoleInfo> Roles { get; init; } = new();
}

public record RoleInfo
{
    public Guid RoleId { get; init; }
    public string Name { get; init; } = string.Empty;
}
