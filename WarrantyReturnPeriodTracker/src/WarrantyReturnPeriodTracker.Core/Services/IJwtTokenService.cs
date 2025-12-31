using WarrantyReturnPeriodTracker.Core.Model.UserAggregate;
using WarrantyReturnPeriodTracker.Core.Model.UserAggregate.Entities;

namespace WarrantyReturnPeriodTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
