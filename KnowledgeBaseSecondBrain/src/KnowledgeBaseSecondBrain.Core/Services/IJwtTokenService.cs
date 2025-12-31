using KnowledgeBaseSecondBrain.Core.Model.UserAggregate;
using KnowledgeBaseSecondBrain.Core.Model.UserAggregate.Entities;

namespace KnowledgeBaseSecondBrain.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
