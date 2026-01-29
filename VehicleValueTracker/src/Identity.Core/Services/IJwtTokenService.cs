using Identity.Core.Models.UserAggregate;

namespace Identity.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<string> roles);
}
