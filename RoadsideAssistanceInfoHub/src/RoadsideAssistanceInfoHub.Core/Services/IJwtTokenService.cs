using RoadsideAssistanceInfoHub.Core.Models.UserAggregate;
using RoadsideAssistanceInfoHub.Core.Models.UserAggregate.Entities;

namespace RoadsideAssistanceInfoHub.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
