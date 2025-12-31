using CampingTripPlanner.Core.Model.UserAggregate;
using CampingTripPlanner.Core.Model.UserAggregate.Entities;

namespace CampingTripPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
