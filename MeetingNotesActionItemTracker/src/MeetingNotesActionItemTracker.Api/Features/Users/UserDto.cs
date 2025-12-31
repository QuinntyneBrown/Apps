using MeetingNotesActionItemTracker.Core.Model.UserAggregate;
using MeetingNotesActionItemTracker.Api.Features.Roles;

namespace MeetingNotesActionItemTracker.Api.Features.Users;

public record UserDto
{
    public Guid UserId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public List<RoleDto> Roles { get; init; } = new();
}

public static class UserExtensions
{
    public static UserDto ToDto(this User user, IEnumerable<Core.Model.UserAggregate.Entities.Role>? roles = null)
    {
        var userRoleIds = user.UserRoles.Select(ur => ur.RoleId).ToHashSet();
        var userRoles = roles?
            .Where(r => userRoleIds.Contains(r.RoleId))
            .Select(r => r.ToDto())
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
