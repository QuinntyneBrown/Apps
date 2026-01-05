using CouplesGoalTracker.Core.Models.UserAggregate;
using CouplesGoalTracker.Core.Models.UserAggregate.Entities;

namespace CouplesGoalTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
