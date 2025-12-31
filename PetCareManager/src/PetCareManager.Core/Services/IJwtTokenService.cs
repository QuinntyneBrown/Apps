using PetCareManager.Core.Model.UserAggregate;
using PetCareManager.Core.Model.UserAggregate.Entities;

namespace PetCareManager.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
