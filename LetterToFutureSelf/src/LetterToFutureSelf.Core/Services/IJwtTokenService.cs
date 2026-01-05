using LetterToFutureSelf.Core.Models.UserAggregate;
using LetterToFutureSelf.Core.Models.UserAggregate.Entities;

namespace LetterToFutureSelf.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
