using RoadsideAssistanceInfoHub.Core.Model.UserAggregate;
using RoadsideAssistanceInfoHub.Core.Model.UserAggregate.Entities;

namespace RoadsideAssistanceInfoHub.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
