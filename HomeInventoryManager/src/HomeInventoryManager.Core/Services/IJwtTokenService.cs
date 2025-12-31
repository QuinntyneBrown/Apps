using HomeInventoryManager.Core.Model.UserAggregate;
using HomeInventoryManager.Core.Model.UserAggregate.Entities;

namespace HomeInventoryManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
