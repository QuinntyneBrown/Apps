using TaxDeductionOrganizer.Core.Model.UserAggregate;
using TaxDeductionOrganizer.Core.Model.UserAggregate.Entities;

namespace TaxDeductionOrganizer.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
