using FocusSessionTracker.Core.Models.UserAggregate;
using FocusSessionTracker.Core.Models.UserAggregate.Entities;

namespace FocusSessionTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
