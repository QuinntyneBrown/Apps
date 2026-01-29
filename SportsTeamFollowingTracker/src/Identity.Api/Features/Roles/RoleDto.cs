namespace Identity.Api.Features.Roles;

public record RoleDto(Guid RoleId, Guid TenantId, string Name);
