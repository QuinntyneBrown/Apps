using PersonalLibraryLessonsLearned.Core.Models.UserAggregate;
using PersonalLibraryLessonsLearned.Core.Models.UserAggregate.Entities;

namespace PersonalLibraryLessonsLearned.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
