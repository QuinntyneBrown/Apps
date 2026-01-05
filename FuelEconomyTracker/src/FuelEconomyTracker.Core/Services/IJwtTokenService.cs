using FuelEconomyTracker.Core.Models.UserAggregate;
using FuelEconomyTracker.Core.Models.UserAggregate.Entities;

namespace FuelEconomyTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
