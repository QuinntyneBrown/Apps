using PersonalHealthDashboard.Core.Model.UserAggregate;
using PersonalHealthDashboard.Core.Model.UserAggregate.Entities;

namespace PersonalHealthDashboard.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
