using AnnualHealthScreeningReminder.Core.Model.UserAggregate;
using AnnualHealthScreeningReminder.Core.Model.UserAggregate.Entities;

namespace AnnualHealthScreeningReminder.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
