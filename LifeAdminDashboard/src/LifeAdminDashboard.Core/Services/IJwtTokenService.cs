using LifeAdminDashboard.Core.Model.UserAggregate;
using LifeAdminDashboard.Core.Model.UserAggregate.Entities;

namespace LifeAdminDashboard.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
