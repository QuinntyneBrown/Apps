using DailyJournalingApp.Core.Models.UserAggregate;
using DailyJournalingApp.Core.Models.UserAggregate.Entities;

namespace DailyJournalingApp.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
