using VehicleValueTracker.Core.Model.UserAggregate;
using VehicleValueTracker.Core.Model.UserAggregate.Entities;

namespace VehicleValueTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
