using CollegeSavingsPlanner.Core.Model.UserAggregate;
using CollegeSavingsPlanner.Core.Model.UserAggregate.Entities;

namespace CollegeSavingsPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
