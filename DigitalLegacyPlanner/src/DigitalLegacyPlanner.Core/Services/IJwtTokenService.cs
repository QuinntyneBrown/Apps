using DigitalLegacyPlanner.Core.Models.UserAggregate;
using DigitalLegacyPlanner.Core.Models.UserAggregate.Entities;

namespace DigitalLegacyPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
