using GiftRegistryOrganizer.Core.Models.UserAggregate;
using GiftRegistryOrganizer.Core.Models.UserAggregate.Entities;

namespace GiftRegistryOrganizer.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
