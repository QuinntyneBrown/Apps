using SleepQualityTracker.Core.Model.UserAggregate;
using SleepQualityTracker.Core.Model.UserAggregate.Entities;

namespace SleepQualityTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
