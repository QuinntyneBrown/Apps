using KidsActivitySportsTracker.Core.Models.UserAggregate;
using KidsActivitySportsTracker.Core.Models.UserAggregate.Entities;

namespace KidsActivitySportsTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
