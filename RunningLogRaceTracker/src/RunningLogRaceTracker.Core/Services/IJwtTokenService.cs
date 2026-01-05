using RunningLogRaceTracker.Core.Models.UserAggregate;
using RunningLogRaceTracker.Core.Models.UserAggregate.Entities;

namespace RunningLogRaceTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
