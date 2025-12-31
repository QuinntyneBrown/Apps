using ApplianceWarrantyManualOrganizer.Core.Model.UserAggregate;
using ApplianceWarrantyManualOrganizer.Core.Model.UserAggregate.Entities;

namespace ApplianceWarrantyManualOrganizer.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
