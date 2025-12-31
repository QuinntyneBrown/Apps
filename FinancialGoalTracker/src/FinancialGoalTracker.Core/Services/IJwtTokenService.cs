using FinancialGoalTracker.Core.Model.UserAggregate;
using FinancialGoalTracker.Core.Model.UserAggregate.Entities;

namespace FinancialGoalTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
