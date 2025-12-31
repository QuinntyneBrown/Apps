using DigitalLegacyPlanner.Core.Model.UserAggregate;
using DigitalLegacyPlanner.Core.Model.UserAggregate.Entities;

namespace DigitalLegacyPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
