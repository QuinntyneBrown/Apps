using ChoreAssignmentTracker.Core.Models.UserAggregate;
using ChoreAssignmentTracker.Core.Models.UserAggregate.Entities;

namespace ChoreAssignmentTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
