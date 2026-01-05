using VehicleMaintenanceLogger.Core.Models.UserAggregate;
using VehicleMaintenanceLogger.Core.Models.UserAggregate.Entities;

namespace VehicleMaintenanceLogger.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
