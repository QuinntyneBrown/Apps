using ConversationStarterApp.Core.Model.UserAggregate;
using ConversationStarterApp.Core.Model.UserAggregate.Entities;

namespace ConversationStarterApp.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
