using SubscriptionAuditTool.Core.Model.UserAggregate;
using SubscriptionAuditTool.Core.Model.UserAggregate.Entities;

namespace SubscriptionAuditTool.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
