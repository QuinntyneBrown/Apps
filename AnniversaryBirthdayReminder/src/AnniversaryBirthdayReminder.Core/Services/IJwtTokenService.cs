using AnniversaryBirthdayReminder.Core.Model.UserAggregate;
using AnniversaryBirthdayReminder.Core.Model.UserAggregate.Entities;

namespace AnniversaryBirthdayReminder.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
