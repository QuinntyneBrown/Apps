using StressMoodTracker.Core.Model.UserAggregate;
using StressMoodTracker.Core.Model.UserAggregate.Entities;

namespace StressMoodTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
