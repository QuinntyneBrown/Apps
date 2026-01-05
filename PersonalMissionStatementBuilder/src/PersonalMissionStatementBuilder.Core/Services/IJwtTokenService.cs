using PersonalMissionStatementBuilder.Core.Models.UserAggregate;
using PersonalMissionStatementBuilder.Core.Models.UserAggregate.Entities;

namespace PersonalMissionStatementBuilder.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
