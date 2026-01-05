using FamilyTreeBuilder.Core.Models.UserAggregate;
using FamilyTreeBuilder.Core.Models.UserAggregate.Entities;

namespace FamilyTreeBuilder.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
