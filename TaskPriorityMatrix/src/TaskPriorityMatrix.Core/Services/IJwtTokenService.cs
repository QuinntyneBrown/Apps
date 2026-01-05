using TaskPriorityMatrix.Core.Models.UserAggregate;
using TaskPriorityMatrix.Core.Models.UserAggregate.Entities;

namespace TaskPriorityMatrix.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
