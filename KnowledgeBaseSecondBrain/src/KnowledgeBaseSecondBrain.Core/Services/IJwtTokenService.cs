using KnowledgeBaseSecondBrain.Core.Models.UserAggregate;
using KnowledgeBaseSecondBrain.Core.Models.UserAggregate.Entities;

namespace KnowledgeBaseSecondBrain.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
