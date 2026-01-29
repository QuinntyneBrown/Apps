namespace Identity.Api.Features.Roles;

public record RoleDto
{
    public Guid RoleId { get; init; }
    public string Name { get; init; } = string.Empty;
}
