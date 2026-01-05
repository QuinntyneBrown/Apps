using ConferenceEventManager.Core.Models.UserAggregate;
using ConferenceEventManager.Core.Models.UserAggregate.Entities;

namespace ConferenceEventManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
