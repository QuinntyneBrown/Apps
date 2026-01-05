using TaxDeductionOrganizer.Core.Models.UserAggregate;
using TaxDeductionOrganizer.Core.Models.UserAggregate.Entities;

namespace TaxDeductionOrganizer.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
