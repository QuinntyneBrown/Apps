using SkillDevelopmentTracker.Core.Models.UserAggregate;
using SkillDevelopmentTracker.Core.Models.UserAggregate.Entities;

namespace SkillDevelopmentTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
