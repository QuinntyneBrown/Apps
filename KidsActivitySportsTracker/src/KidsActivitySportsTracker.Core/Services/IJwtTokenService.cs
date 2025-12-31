using KidsActivitySportsTracker.Core.Model.UserAggregate;
using KidsActivitySportsTracker.Core.Model.UserAggregate.Entities;

namespace KidsActivitySportsTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
