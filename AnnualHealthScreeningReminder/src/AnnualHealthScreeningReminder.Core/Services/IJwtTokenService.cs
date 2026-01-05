using AnnualHealthScreeningReminder.Core.Models.UserAggregate;
using AnnualHealthScreeningReminder.Core.Models.UserAggregate.Entities;

namespace AnnualHealthScreeningReminder.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
