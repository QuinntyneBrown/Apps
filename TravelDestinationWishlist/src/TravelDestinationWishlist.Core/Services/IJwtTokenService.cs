using TravelDestinationWishlist.Core.Models.UserAggregate;
using TravelDestinationWishlist.Core.Models.UserAggregate.Entities;

namespace TravelDestinationWishlist.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
