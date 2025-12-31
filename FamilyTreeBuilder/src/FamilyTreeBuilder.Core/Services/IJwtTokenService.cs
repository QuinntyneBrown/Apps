using FamilyTreeBuilder.Core.Model.UserAggregate;
using FamilyTreeBuilder.Core.Model.UserAggregate.Entities;

namespace FamilyTreeBuilder.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
