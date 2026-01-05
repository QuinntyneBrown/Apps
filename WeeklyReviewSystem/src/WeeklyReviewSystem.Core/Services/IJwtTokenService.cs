using WeeklyReviewSystem.Core.Models.UserAggregate;
using WeeklyReviewSystem.Core.Models.UserAggregate.Entities;

namespace WeeklyReviewSystem.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
