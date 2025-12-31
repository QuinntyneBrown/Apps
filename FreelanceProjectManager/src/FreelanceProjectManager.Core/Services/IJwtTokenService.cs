using FreelanceProjectManager.Core.Model.UserAggregate;
using FreelanceProjectManager.Core.Model.UserAggregate.Entities;

namespace FreelanceProjectManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
