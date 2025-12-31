using SalaryCompensationTracker.Core.Model.UserAggregate;
using SalaryCompensationTracker.Core.Model.UserAggregate.Entities;

namespace SalaryCompensationTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
