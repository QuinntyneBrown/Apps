using TimeAuditTracker.Core.Models.UserAggregate;
using TimeAuditTracker.Core.Models.UserAggregate.Entities;

namespace TimeAuditTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
