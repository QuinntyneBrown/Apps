using ClassicCarRestorationLog.Core.Model.UserAggregate;
using ClassicCarRestorationLog.Core.Model.UserAggregate.Entities;

namespace ClassicCarRestorationLog.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
