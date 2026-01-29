using Identity.Core.Models.UserAggregate.Entities;

namespace Identity.Api.Features.Roles;

public record RoleDto(Guid RoleId, Guid TenantId, string Name);

public static class RoleExtensions
{
    public static RoleDto ToDto(this Role role)
    {
        return new RoleDto(role.RoleId, role.TenantId, role.Name);
    }
}
