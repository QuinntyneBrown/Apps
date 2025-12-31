using RealEstateInvestmentAnalyzer.Core.Model.UserAggregate;
using RealEstateInvestmentAnalyzer.Core.Model.UserAggregate.Entities;

namespace RealEstateInvestmentAnalyzer.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
