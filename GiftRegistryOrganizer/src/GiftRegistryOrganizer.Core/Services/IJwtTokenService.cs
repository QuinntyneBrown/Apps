using GiftRegistryOrganizer.Core.Model.UserAggregate;
using GiftRegistryOrganizer.Core.Model.UserAggregate.Entities;

namespace GiftRegistryOrganizer.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
