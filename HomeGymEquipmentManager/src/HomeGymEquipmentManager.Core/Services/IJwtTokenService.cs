using HomeGymEquipmentManager.Core.Models.UserAggregate;
using HomeGymEquipmentManager.Core.Models.UserAggregate.Entities;

namespace HomeGymEquipmentManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
