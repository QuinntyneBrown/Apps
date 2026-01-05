using WorkoutPlanBuilder.Core.Models.UserAggregate;
using WorkoutPlanBuilder.Core.Models.UserAggregate.Entities;

namespace WorkoutPlanBuilder.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
