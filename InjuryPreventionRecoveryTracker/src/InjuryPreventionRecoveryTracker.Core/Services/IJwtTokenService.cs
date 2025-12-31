using InjuryPreventionRecoveryTracker.Core.Model.UserAggregate;
using InjuryPreventionRecoveryTracker.Core.Model.UserAggregate.Entities;

namespace InjuryPreventionRecoveryTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
