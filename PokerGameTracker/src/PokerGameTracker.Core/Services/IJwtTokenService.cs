using PokerGameTracker.Core.Model.UserAggregate;
using PokerGameTracker.Core.Model.UserAggregate.Entities;

namespace PokerGameTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
