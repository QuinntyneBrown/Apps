using FishingLogSpotTracker.Core.Model.UserAggregate;
using FishingLogSpotTracker.Core.Model.UserAggregate.Entities;

namespace FishingLogSpotTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
