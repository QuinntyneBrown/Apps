using StressMoodTracker.Core.Models.UserAggregate;
using StressMoodTracker.Core.Models.UserAggregate.Entities;

namespace StressMoodTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
