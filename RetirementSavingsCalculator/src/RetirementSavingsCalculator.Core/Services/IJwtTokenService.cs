using RetirementSavingsCalculator.Core.Models.UserAggregate;
using RetirementSavingsCalculator.Core.Models.UserAggregate.Entities;

namespace RetirementSavingsCalculator.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
