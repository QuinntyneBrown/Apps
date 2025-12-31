using VideoGameCollectionManager.Core.Model.UserAggregate;
using VideoGameCollectionManager.Core.Model.UserAggregate.Entities;

namespace VideoGameCollectionManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
