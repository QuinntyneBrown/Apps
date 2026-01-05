using FreelanceProjectManager.Core.Models.UserAggregate;
using FreelanceProjectManager.Core.Models.UserAggregate.Entities;

namespace FreelanceProjectManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
