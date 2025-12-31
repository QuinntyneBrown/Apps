using HomeImprovementProjectManager.Core.Model.UserAggregate;
using HomeImprovementProjectManager.Core.Model.UserAggregate.Entities;

namespace HomeImprovementProjectManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
