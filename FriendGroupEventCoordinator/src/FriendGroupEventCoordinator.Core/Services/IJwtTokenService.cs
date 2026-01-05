using FriendGroupEventCoordinator.Core.Models.UserAggregate;
using FriendGroupEventCoordinator.Core.Models.UserAggregate.Entities;

namespace FriendGroupEventCoordinator.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
