using LifeAdminDashboard.Core.Models.UserAggregate;
using LifeAdminDashboard.Core.Models.UserAggregate.Entities;

namespace LifeAdminDashboard.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
