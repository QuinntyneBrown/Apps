using SideHustleIncomeTracker.Core.Models.UserAggregate;
using SideHustleIncomeTracker.Core.Models.UserAggregate.Entities;

namespace SideHustleIncomeTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
