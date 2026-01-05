using AnniversaryBirthdayReminder.Core.Models.UserAggregate;
using AnniversaryBirthdayReminder.Core.Models.UserAggregate.Entities;

namespace AnniversaryBirthdayReminder.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
