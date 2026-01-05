using FamilyVacationPlanner.Core.Models.UserAggregate;
using FamilyVacationPlanner.Core.Models.UserAggregate.Entities;

namespace FamilyVacationPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
