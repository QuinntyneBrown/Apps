using SportsTeamFollowingTracker.Core.Model.UserAggregate;
using SportsTeamFollowingTracker.Core.Model.UserAggregate.Entities;

namespace SportsTeamFollowingTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
