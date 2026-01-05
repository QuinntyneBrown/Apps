using GolfScoreTracker.Core.Models.UserAggregate;
using GolfScoreTracker.Core.Models.UserAggregate.Entities;

namespace GolfScoreTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
