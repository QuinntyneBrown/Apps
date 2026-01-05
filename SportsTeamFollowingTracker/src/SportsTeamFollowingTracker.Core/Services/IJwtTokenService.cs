using SportsTeamFollowingTracker.Core.Models.UserAggregate;
using SportsTeamFollowingTracker.Core.Models.UserAggregate.Entities;

namespace SportsTeamFollowingTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
