namespace Identity.Api.Features.Users;

public record UserDto
{
    public Guid UserId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public List<string> Roles { get; init; } = new();
}
