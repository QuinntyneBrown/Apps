using InvestmentPortfolioTracker.Core.Model.UserAggregate;
using InvestmentPortfolioTracker.Core.Model.UserAggregate.Entities;

namespace InvestmentPortfolioTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
