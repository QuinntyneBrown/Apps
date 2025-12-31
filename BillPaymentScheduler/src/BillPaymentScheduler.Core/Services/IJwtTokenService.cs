using BillPaymentScheduler.Core.Model.UserAggregate;
using BillPaymentScheduler.Core.Model.UserAggregate.Entities;

namespace BillPaymentScheduler.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
