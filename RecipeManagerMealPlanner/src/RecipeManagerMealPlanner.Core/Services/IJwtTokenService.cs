using RecipeManagerMealPlanner.Core.Models.UserAggregate;
using RecipeManagerMealPlanner.Core.Models.UserAggregate.Entities;

namespace RecipeManagerMealPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
