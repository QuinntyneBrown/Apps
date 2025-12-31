using PhotographySessionLogger.Core.Model.UserAggregate;
using PhotographySessionLogger.Core.Model.UserAggregate.Entities;

namespace PhotographySessionLogger.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
