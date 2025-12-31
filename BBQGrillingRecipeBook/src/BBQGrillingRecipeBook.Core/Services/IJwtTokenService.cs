using BBQGrillingRecipeBook.Core.Model.UserAggregate;
using BBQGrillingRecipeBook.Core.Model.UserAggregate.Entities;

namespace BBQGrillingRecipeBook.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
