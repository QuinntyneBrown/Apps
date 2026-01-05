using VideoGameCollectionManager.Core.Models.UserAggregate;
using VideoGameCollectionManager.Core.Models.UserAggregate.Entities;

namespace VideoGameCollectionManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
