using HomeMaintenanceSchedule.Core.Model.UserAggregate;
using HomeMaintenanceSchedule.Core.Model.UserAggregate.Entities;

namespace HomeMaintenanceSchedule.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
