using DateNightIdeaGenerator.Core.Model.UserAggregate;
using DateNightIdeaGenerator.Core.Model.UserAggregate.Entities;

namespace DateNightIdeaGenerator.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
