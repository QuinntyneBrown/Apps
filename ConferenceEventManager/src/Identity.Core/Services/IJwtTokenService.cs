using Identity.Core.Models;

namespace Identity.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<string> roles);
}
