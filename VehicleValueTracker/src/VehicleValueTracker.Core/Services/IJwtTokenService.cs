using VehicleValueTracker.Core.Models.UserAggregate;
using VehicleValueTracker.Core.Models.UserAggregate.Entities;

namespace VehicleValueTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
