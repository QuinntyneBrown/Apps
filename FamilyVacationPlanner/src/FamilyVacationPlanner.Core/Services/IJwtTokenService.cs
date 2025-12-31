using FamilyVacationPlanner.Core.Model.UserAggregate;
using FamilyVacationPlanner.Core.Model.UserAggregate.Entities;

namespace FamilyVacationPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
