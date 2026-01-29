using Identity.Core.Models.UserAggregate;
using Identity.Core.Models.UserAggregate.Entities;

namespace Identity.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
