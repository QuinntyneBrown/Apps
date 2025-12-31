using HydrationTracker.Core.Model.UserAggregate;
using HydrationTracker.Core.Model.UserAggregate.Entities;

namespace HydrationTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
