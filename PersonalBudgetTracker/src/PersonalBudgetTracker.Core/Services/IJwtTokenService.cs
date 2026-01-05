using PersonalBudgetTracker.Core.Models.UserAggregate;
using PersonalBudgetTracker.Core.Models.UserAggregate.Entities;

namespace PersonalBudgetTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
