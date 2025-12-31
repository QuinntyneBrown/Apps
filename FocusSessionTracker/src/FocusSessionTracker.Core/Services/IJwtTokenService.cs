using FocusSessionTracker.Core.Model.UserAggregate;
using FocusSessionTracker.Core.Model.UserAggregate.Entities;

namespace FocusSessionTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
