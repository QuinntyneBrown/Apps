using NeighborhoodSocialNetwork.Core.Model.UserAggregate;
using NeighborhoodSocialNetwork.Core.Model.UserAggregate.Entities;

namespace NeighborhoodSocialNetwork.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
