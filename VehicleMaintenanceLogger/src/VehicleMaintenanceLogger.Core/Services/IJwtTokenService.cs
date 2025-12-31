using VehicleMaintenanceLogger.Core.Model.UserAggregate;
using VehicleMaintenanceLogger.Core.Model.UserAggregate.Entities;

namespace VehicleMaintenanceLogger.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
