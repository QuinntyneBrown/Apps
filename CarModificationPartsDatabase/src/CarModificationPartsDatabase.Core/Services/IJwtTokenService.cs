using CarModificationPartsDatabase.Core.Model.UserAggregate;
using CarModificationPartsDatabase.Core.Model.UserAggregate.Entities;

namespace CarModificationPartsDatabase.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
