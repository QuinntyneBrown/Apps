using ExpenseClaimSystem.Core.Models.UserAggregate;
using ExpenseClaimSystem.Core.Models.UserAggregate.Entities;

namespace ExpenseClaimSystem.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
