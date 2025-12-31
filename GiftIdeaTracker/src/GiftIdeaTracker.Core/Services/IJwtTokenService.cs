using GiftIdeaTracker.Core.Model.UserAggregate;
using GiftIdeaTracker.Core.Model.UserAggregate.Entities;

namespace GiftIdeaTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
