using PasswordAccountAuditor.Core.Model.UserAggregate;
using PasswordAccountAuditor.Core.Model.UserAggregate.Entities;

namespace PasswordAccountAuditor.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
