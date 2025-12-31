using GolfScoreTracker.Core.Model.UserAggregate;
using GolfScoreTracker.Core.Model.UserAggregate.Entities;

namespace GolfScoreTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
