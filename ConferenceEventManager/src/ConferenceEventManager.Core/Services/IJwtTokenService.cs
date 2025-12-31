using ConferenceEventManager.Core.Model.UserAggregate;
using ConferenceEventManager.Core.Model.UserAggregate.Entities;

namespace ConferenceEventManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
