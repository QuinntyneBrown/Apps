using ResumeCareerAchievementTracker.Core.Models.UserAggregate;
using ResumeCareerAchievementTracker.Core.Models.UserAggregate.Entities;

namespace ResumeCareerAchievementTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
