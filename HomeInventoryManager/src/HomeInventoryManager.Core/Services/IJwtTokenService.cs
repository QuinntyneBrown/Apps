using HomeInventoryManager.Core.Models.UserAggregate;
using HomeInventoryManager.Core.Models.UserAggregate.Entities;

namespace HomeInventoryManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
