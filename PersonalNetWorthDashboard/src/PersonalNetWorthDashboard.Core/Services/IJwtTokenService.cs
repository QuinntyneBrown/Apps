using PersonalNetWorthDashboard.Core.Models.UserAggregate;
using PersonalNetWorthDashboard.Core.Models.UserAggregate.Entities;

namespace PersonalNetWorthDashboard.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
