using BloodPressureMonitor.Core.Models.UserAggregate;
using BloodPressureMonitor.Core.Models.UserAggregate.Entities;

namespace BloodPressureMonitor.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
