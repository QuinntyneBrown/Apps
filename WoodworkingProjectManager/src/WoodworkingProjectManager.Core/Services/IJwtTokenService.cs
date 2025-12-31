using WoodworkingProjectManager.Core.Model.UserAggregate;
using WoodworkingProjectManager.Core.Model.UserAggregate.Entities;

namespace WoodworkingProjectManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
