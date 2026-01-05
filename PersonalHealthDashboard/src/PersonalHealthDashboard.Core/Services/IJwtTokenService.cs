using PersonalHealthDashboard.Core.Models.UserAggregate;
using PersonalHealthDashboard.Core.Models.UserAggregate.Entities;

namespace PersonalHealthDashboard.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
