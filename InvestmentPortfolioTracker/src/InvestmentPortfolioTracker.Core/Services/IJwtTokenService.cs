using InvestmentPortfolioTracker.Core.Models.UserAggregate;
using InvestmentPortfolioTracker.Core.Models.UserAggregate.Entities;

namespace InvestmentPortfolioTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
