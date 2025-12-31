using ResumeCareerAchievementTracker.Core.Model.UserAggregate;
using ResumeCareerAchievementTracker.Core.Model.UserAggregate.Entities;

namespace ResumeCareerAchievementTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
