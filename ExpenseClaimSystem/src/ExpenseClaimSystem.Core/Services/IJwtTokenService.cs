using ExpenseClaimSystem.Core.Model.UserAggregate;
using ExpenseClaimSystem.Core.Model.UserAggregate.Entities;

namespace ExpenseClaimSystem.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
