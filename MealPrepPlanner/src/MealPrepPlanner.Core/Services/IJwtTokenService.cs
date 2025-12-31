using MealPrepPlanner.Core.Model.UserAggregate;
using MealPrepPlanner.Core.Model.UserAggregate.Entities;

namespace MealPrepPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
