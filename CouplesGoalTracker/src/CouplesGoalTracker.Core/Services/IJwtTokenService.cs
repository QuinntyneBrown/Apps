using CouplesGoalTracker.Core.Model.UserAggregate;
using CouplesGoalTracker.Core.Model.UserAggregate.Entities;

namespace CouplesGoalTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
