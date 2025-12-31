using ProfessionalNetworkCRM.Core.Model.UserAggregate;
using ProfessionalNetworkCRM.Core.Model.UserAggregate.Entities;

namespace ProfessionalNetworkCRM.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
