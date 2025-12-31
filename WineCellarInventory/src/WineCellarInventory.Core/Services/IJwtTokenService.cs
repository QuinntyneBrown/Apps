using WineCellarInventory.Core.Model.UserAggregate;
using WineCellarInventory.Core.Model.UserAggregate.Entities;

namespace WineCellarInventory.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
