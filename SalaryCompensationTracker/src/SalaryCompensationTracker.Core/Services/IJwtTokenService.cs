using SalaryCompensationTracker.Core.Models.UserAggregate;
using SalaryCompensationTracker.Core.Models.UserAggregate.Entities;

namespace SalaryCompensationTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
