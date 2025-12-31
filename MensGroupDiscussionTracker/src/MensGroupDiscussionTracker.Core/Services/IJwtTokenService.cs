using MensGroupDiscussionTracker.Core.Model.UserAggregate;
using MensGroupDiscussionTracker.Core.Model.UserAggregate.Entities;

namespace MensGroupDiscussionTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
