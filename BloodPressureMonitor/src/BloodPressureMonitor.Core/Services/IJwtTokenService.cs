using BloodPressureMonitor.Core.Model.UserAggregate;
using BloodPressureMonitor.Core.Model.UserAggregate.Entities;

namespace BloodPressureMonitor.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
