using DocumentVaultOrganizer.Core.Model.UserAggregate;
using DocumentVaultOrganizer.Core.Model.UserAggregate.Entities;

namespace DocumentVaultOrganizer.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
