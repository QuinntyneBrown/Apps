using FamilyCalendarEventPlanner.Core.Model.UserAggregate;

namespace FamilyCalendarEventPlanner.Api.Features.Users;

public record UserDto
{
    public Guid UserId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public List<RoleDto> Roles { get; init; } = new();
}

public record RoleDto
{
    public Guid RoleId { get; init; }
    public string Name { get; init; } = string.Empty;
}

public static class UserExtensions
{
    public static UserDto ToDto(this User user, IEnumerable<Core.Model.UserAggregate.Entities.Role>? roles = null)
    {
        var userRoleIds = user.UserRoles.Select(ur => ur.RoleId).ToHashSet();
        var userRoles = roles?
            .Where(r => userRoleIds.Contains(r.RoleId))
            .Select(r => new RoleDto { RoleId = r.RoleId, Name = r.Name })
            .ToList() ?? new List<RoleDto>();

        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            Roles = userRoles
        };
    }
}

public static class RoleExtensions
{
    public static RoleDto ToDto(this Core.Model.UserAggregate.Entities.Role role)
    {
        return new RoleDto
        {
            RoleId = role.RoleId,
            Name = role.Name
        };
    }
}
