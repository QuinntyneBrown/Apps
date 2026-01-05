using PersonalProjectPipeline.Core.Models.UserAggregate;
using PersonalProjectPipeline.Core.Models.UserAggregate.Entities;

namespace PersonalProjectPipeline.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
