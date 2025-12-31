using HomeBrewingTracker.Core.Model.UserAggregate;
using HomeBrewingTracker.Core.Model.UserAggregate.Entities;

namespace HomeBrewingTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
