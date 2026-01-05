using PersonalWiki.Core.Models.UserAggregate;
using PersonalWiki.Core.Models.UserAggregate.Entities;

namespace PersonalWiki.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
