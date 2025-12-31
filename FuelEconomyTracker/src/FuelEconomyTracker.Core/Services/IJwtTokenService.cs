using FuelEconomyTracker.Core.Model.UserAggregate;
using FuelEconomyTracker.Core.Model.UserAggregate.Entities;

namespace FuelEconomyTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
