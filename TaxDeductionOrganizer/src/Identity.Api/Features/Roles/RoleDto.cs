namespace Identity.Api.Features.Roles;

public record RoleDto
{
    public Guid RoleId { get; init; }
    public Guid TenantId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
}
