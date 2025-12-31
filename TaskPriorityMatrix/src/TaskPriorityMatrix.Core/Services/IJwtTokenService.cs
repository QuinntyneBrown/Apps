using TaskPriorityMatrix.Core.Model.UserAggregate;
using TaskPriorityMatrix.Core.Model.UserAggregate.Entities;

namespace TaskPriorityMatrix.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
