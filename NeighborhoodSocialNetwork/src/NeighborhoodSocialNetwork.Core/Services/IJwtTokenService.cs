using NeighborhoodSocialNetwork.Core.Models.UserAggregate;
using NeighborhoodSocialNetwork.Core.Models.UserAggregate.Entities;

namespace NeighborhoodSocialNetwork.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
