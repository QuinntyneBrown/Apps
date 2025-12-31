using MorningRoutineBuilder.Core.Model.UserAggregate;
using MorningRoutineBuilder.Core.Model.UserAggregate.Entities;

namespace MorningRoutineBuilder.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
