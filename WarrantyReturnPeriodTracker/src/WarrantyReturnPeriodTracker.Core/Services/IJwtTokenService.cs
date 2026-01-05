using WarrantyReturnPeriodTracker.Core.Models.UserAggregate;
using WarrantyReturnPeriodTracker.Core.Models.UserAggregate.Entities;

namespace WarrantyReturnPeriodTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
