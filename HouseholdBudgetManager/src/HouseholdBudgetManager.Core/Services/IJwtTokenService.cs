using HouseholdBudgetManager.Core.Models.UserAggregate;
using HouseholdBudgetManager.Core.Models.UserAggregate.Entities;

namespace HouseholdBudgetManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
