using WorkoutPlanBuilder.Core.Model.UserAggregate;
using WorkoutPlanBuilder.Core.Model.UserAggregate.Entities;

namespace WorkoutPlanBuilder.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
