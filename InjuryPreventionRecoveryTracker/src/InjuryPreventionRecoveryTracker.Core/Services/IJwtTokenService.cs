using InjuryPreventionRecoveryTracker.Core.Models.UserAggregate;
using InjuryPreventionRecoveryTracker.Core.Models.UserAggregate.Entities;

namespace InjuryPreventionRecoveryTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
