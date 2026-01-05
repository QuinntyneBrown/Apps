using BillPaymentScheduler.Core.Models.UserAggregate;
using BillPaymentScheduler.Core.Models.UserAggregate.Entities;

namespace BillPaymentScheduler.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
