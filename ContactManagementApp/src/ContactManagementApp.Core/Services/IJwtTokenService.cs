using ContactManagementApp.Core.Model.UserAggregate;
using ContactManagementApp.Core.Model.UserAggregate.Entities;

namespace ContactManagementApp.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
