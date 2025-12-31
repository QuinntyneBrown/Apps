using SideHustleIncomeTracker.Core.Model.UserAggregate;
using SideHustleIncomeTracker.Core.Model.UserAggregate.Entities;

namespace SideHustleIncomeTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
