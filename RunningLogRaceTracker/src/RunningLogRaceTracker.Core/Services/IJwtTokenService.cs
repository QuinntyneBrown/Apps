using RunningLogRaceTracker.Core.Model.UserAggregate;
using RunningLogRaceTracker.Core.Model.UserAggregate.Entities;

namespace RunningLogRaceTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
