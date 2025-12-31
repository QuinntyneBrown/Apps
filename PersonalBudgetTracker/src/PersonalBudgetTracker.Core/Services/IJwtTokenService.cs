using PersonalBudgetTracker.Core.Model.UserAggregate;
using PersonalBudgetTracker.Core.Model.UserAggregate.Entities;

namespace PersonalBudgetTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
