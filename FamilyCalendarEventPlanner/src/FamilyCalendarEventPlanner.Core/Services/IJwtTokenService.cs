using FamilyCalendarEventPlanner.Core.Model.UserAggregate;
using FamilyCalendarEventPlanner.Core.Model.UserAggregate.Entities;

namespace FamilyCalendarEventPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
