using MealPrepPlanner.Core.Models.UserAggregate;
using MealPrepPlanner.Core.Models.UserAggregate.Entities;

namespace MealPrepPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
