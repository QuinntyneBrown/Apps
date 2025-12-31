using HomeGymEquipmentManager.Core.Model.UserAggregate;
using HomeGymEquipmentManager.Core.Model.UserAggregate.Entities;

namespace HomeGymEquipmentManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
