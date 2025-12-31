using MotorcycleRideLog.Core.Model.UserAggregate;
using MotorcycleRideLog.Core.Model.UserAggregate.Entities;

namespace MotorcycleRideLog.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
