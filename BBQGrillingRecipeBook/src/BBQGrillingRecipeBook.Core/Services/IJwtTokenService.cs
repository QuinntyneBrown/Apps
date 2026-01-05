using BBQGrillingRecipeBook.Core.Models.UserAggregate;
using BBQGrillingRecipeBook.Core.Models.UserAggregate.Entities;

namespace BBQGrillingRecipeBook.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
