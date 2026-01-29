using Identity.Core.Models.UserAggregate;

namespace Identity.Api.Features.Users;

public record UserDto(Guid UserId, Guid TenantId, string UserName, string Email);

public static class UserExtensions
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto(user.UserId, user.TenantId, user.UserName, user.Email);
    }
}
