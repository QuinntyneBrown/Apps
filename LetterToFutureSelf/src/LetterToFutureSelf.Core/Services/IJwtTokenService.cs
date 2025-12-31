using LetterToFutureSelf.Core.Model.UserAggregate;
using LetterToFutureSelf.Core.Model.UserAggregate.Entities;

namespace LetterToFutureSelf.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
