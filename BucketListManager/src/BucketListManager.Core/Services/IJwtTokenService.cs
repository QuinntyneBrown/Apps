using BucketListManager.Core.Models.UserAggregate;
using BucketListManager.Core.Models.UserAggregate.Entities;

namespace BucketListManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
