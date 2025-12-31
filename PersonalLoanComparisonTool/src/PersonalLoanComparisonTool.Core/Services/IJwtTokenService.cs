using PersonalLoanComparisonTool.Core.Model.UserAggregate;
using PersonalLoanComparisonTool.Core.Model.UserAggregate.Entities;

namespace PersonalLoanComparisonTool.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
