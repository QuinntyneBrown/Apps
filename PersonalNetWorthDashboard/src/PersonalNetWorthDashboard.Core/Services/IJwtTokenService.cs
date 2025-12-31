using PersonalNetWorthDashboard.Core.Model.UserAggregate;
using PersonalNetWorthDashboard.Core.Model.UserAggregate.Entities;

namespace PersonalNetWorthDashboard.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
