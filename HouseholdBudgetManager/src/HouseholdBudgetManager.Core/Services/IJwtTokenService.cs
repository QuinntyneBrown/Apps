using HouseholdBudgetManager.Core.Model.UserAggregate;
using HouseholdBudgetManager.Core.Model.UserAggregate.Entities;

namespace HouseholdBudgetManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
