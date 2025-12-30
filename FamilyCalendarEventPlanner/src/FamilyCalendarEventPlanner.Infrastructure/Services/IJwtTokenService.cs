using FamilyCalendarEventPlanner.Core.Model.UserAggregate;
using FamilyCalendarEventPlanner.Core.Model.UserAggregate.Entities;

namespace FamilyCalendarEventPlanner.Infrastructure.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
