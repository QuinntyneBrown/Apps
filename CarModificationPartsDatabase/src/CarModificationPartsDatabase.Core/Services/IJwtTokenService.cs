using CarModificationPartsDatabase.Core.Models.UserAggregate;
using CarModificationPartsDatabase.Core.Models.UserAggregate.Entities;

namespace CarModificationPartsDatabase.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
