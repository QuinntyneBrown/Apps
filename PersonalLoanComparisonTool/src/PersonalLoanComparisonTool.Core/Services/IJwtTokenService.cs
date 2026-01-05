using PersonalLoanComparisonTool.Core.Models.UserAggregate;
using PersonalLoanComparisonTool.Core.Models.UserAggregate.Entities;

namespace PersonalLoanComparisonTool.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
