using FriendGroupEventCoordinator.Core.Model.UserAggregate;
using FriendGroupEventCoordinator.Core.Model.UserAggregate.Entities;

namespace FriendGroupEventCoordinator.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
