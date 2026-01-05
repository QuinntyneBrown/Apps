using HabitFormationApp.Core.Models.UserAggregate;
using HabitFormationApp.Core.Models.UserAggregate.Entities;

namespace HabitFormationApp.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
