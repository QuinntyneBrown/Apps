using SkillDevelopmentTracker.Core.Model.UserAggregate;
using SkillDevelopmentTracker.Core.Model.UserAggregate.Entities;

namespace SkillDevelopmentTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
