using CollegeSavingsPlanner.Core.Models.UserAggregate;
using CollegeSavingsPlanner.Core.Models.UserAggregate.Entities;

namespace CollegeSavingsPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
