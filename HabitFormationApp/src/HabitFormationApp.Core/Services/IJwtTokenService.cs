using HabitFormationApp.Core.Model.UserAggregate;
using HabitFormationApp.Core.Model.UserAggregate.Entities;

namespace HabitFormationApp.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
