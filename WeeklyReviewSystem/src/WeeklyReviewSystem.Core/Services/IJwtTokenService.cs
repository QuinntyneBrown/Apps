using WeeklyReviewSystem.Core.Model.UserAggregate;
using WeeklyReviewSystem.Core.Model.UserAggregate.Entities;

namespace WeeklyReviewSystem.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
