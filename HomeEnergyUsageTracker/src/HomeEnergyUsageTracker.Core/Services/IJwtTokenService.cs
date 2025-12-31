using HomeEnergyUsageTracker.Core.Model.UserAggregate;
using HomeEnergyUsageTracker.Core.Model.UserAggregate.Entities;

namespace HomeEnergyUsageTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
