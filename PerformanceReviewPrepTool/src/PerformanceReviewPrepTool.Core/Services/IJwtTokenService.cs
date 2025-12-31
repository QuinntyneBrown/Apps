using PerformanceReviewPrepTool.Core.Model.UserAggregate;
using PerformanceReviewPrepTool.Core.Model.UserAggregate.Entities;

namespace PerformanceReviewPrepTool.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
