using BucketListManager.Core.Model.UserAggregate;
using BucketListManager.Core.Model.UserAggregate.Entities;

namespace BucketListManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
