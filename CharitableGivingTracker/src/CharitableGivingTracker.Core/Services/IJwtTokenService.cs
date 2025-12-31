using CharitableGivingTracker.Core.Model.UserAggregate;
using CharitableGivingTracker.Core.Model.UserAggregate.Entities;

namespace CharitableGivingTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
