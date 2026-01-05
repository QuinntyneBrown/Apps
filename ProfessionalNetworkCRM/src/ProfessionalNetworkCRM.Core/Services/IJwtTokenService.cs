using ProfessionalNetworkCRM.Core.Models.UserAggregate;
using ProfessionalNetworkCRM.Core.Models.UserAggregate.Entities;

namespace ProfessionalNetworkCRM.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
