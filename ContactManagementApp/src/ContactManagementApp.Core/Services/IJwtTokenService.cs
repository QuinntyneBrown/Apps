using ContactManagementApp.Core.Models.UserAggregate;
using ContactManagementApp.Core.Models.UserAggregate.Entities;

namespace ContactManagementApp.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
