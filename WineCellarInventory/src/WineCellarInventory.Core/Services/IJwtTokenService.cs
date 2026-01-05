using WineCellarInventory.Core.Models.UserAggregate;
using WineCellarInventory.Core.Models.UserAggregate.Entities;

namespace WineCellarInventory.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
