using RecipeManagerMealPlanner.Core.Model.UserAggregate;
using RecipeManagerMealPlanner.Core.Model.UserAggregate.Entities;

namespace RecipeManagerMealPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
