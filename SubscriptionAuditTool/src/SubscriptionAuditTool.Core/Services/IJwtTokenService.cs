using SubscriptionAuditTool.Core.Models.UserAggregate;
using SubscriptionAuditTool.Core.Models.UserAggregate.Entities;

namespace SubscriptionAuditTool.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
