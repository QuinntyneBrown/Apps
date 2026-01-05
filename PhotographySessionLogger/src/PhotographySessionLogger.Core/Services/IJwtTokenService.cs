using PhotographySessionLogger.Core.Models.UserAggregate;
using PhotographySessionLogger.Core.Models.UserAggregate.Entities;

namespace PhotographySessionLogger.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
