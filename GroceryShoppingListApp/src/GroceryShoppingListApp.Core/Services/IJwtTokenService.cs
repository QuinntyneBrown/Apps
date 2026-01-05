using GroceryShoppingListApp.Core.Models.UserAggregate;
using GroceryShoppingListApp.Core.Models.UserAggregate.Entities;

namespace GroceryShoppingListApp.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
