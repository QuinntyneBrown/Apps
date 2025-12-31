using TravelDestinationWishlist.Core.Model.UserAggregate;
using TravelDestinationWishlist.Core.Model.UserAggregate.Entities;

namespace TravelDestinationWishlist.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
