using MorningRoutineBuilder.Core.Models.UserAggregate;
using MorningRoutineBuilder.Core.Models.UserAggregate.Entities;

namespace MorningRoutineBuilder.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
