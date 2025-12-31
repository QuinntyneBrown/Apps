using RetirementSavingsCalculator.Core.Model.UserAggregate;
using RetirementSavingsCalculator.Core.Model.UserAggregate.Entities;

namespace RetirementSavingsCalculator.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
