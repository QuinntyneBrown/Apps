using CryptoPortfolioManager.Core.Model.UserAggregate;
using CryptoPortfolioManager.Core.Model.UserAggregate.Entities;

namespace CryptoPortfolioManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
