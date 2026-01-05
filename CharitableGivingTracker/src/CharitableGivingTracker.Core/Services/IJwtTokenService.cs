using CharitableGivingTracker.Core.Models.UserAggregate;
using CharitableGivingTracker.Core.Models.UserAggregate.Entities;

namespace CharitableGivingTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
