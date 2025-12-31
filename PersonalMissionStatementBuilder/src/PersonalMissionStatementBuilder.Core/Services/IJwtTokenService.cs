using PersonalMissionStatementBuilder.Core.Model.UserAggregate;
using PersonalMissionStatementBuilder.Core.Model.UserAggregate.Entities;

namespace PersonalMissionStatementBuilder.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
