using GroceryShoppingListApp.Core.Model.UserAggregate;
using GroceryShoppingListApp.Core.Model.UserAggregate.Entities;

namespace GroceryShoppingListApp.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
