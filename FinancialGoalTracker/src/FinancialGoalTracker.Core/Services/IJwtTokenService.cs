using FinancialGoalTracker.Core.Models.UserAggregate;
using FinancialGoalTracker.Core.Models.UserAggregate.Entities;

namespace FinancialGoalTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
