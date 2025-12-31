using TimeAuditTracker.Core.Model.UserAggregate;
using TimeAuditTracker.Core.Model.UserAggregate.Entities;

namespace TimeAuditTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
