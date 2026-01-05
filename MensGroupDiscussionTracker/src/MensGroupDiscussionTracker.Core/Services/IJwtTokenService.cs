using MensGroupDiscussionTracker.Core.Models.UserAggregate;
using MensGroupDiscussionTracker.Core.Models.UserAggregate.Entities;

namespace MensGroupDiscussionTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
