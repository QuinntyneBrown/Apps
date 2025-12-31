using GiftRegistryOrganizer.Core.Model.UserAggregate.Entities;

namespace GiftRegistryOrganizer.Api.Features.Roles;

public record RoleDto
{
    public Guid RoleId { get; init; }
    public string Name { get; init; } = string.Empty;
}

public static class RoleExtensions
{
    public static RoleDto ToDto(this Role role)
    {
        return new RoleDto
        {
            RoleId = role.RoleId,
            Name = role.Name
        };
    }
}
