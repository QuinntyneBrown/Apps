using CampingTripPlanner.Core.Models.UserAggregate;
using CampingTripPlanner.Core.Models.UserAggregate.Entities;

namespace CampingTripPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
