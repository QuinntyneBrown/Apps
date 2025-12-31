using DailyJournalingApp.Core.Model.UserAggregate;
using DailyJournalingApp.Core.Model.UserAggregate.Entities;

namespace DailyJournalingApp.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
