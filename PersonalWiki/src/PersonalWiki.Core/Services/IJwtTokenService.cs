using PersonalWiki.Core.Model.UserAggregate;
using PersonalWiki.Core.Model.UserAggregate.Entities;

namespace PersonalWiki.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
