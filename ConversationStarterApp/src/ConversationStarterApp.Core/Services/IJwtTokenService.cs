using ConversationStarterApp.Core.Models.UserAggregate;
using ConversationStarterApp.Core.Models.UserAggregate.Entities;

namespace ConversationStarterApp.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
