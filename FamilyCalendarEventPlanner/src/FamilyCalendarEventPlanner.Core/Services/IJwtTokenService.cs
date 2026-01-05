using FamilyCalendarEventPlanner.Core.Models.UserAggregate;
using FamilyCalendarEventPlanner.Core.Models.UserAggregate.Entities;

namespace FamilyCalendarEventPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
