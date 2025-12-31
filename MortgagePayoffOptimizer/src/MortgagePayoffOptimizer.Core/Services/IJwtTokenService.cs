using MortgagePayoffOptimizer.Core.Model.UserAggregate;
using MortgagePayoffOptimizer.Core.Model.UserAggregate.Entities;

namespace MortgagePayoffOptimizer.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
