using PersonalProjectPipeline.Core.Model.UserAggregate;
using PersonalProjectPipeline.Core.Model.UserAggregate.Entities;

namespace PersonalProjectPipeline.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
