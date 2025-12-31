using ChoreAssignmentTracker.Core.Model.UserAggregate;
using ChoreAssignmentTracker.Core.Model.UserAggregate.Entities;

namespace ChoreAssignmentTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
