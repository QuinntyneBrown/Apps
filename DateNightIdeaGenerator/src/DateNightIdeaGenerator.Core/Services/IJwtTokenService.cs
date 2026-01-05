using DateNightIdeaGenerator.Core.Models.UserAggregate;
using DateNightIdeaGenerator.Core.Models.UserAggregate.Entities;

namespace DateNightIdeaGenerator.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
