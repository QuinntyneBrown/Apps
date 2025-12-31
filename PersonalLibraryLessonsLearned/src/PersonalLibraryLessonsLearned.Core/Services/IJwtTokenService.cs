using PersonalLibraryLessonsLearned.Core.Model.UserAggregate;
using PersonalLibraryLessonsLearned.Core.Model.UserAggregate.Entities;

namespace PersonalLibraryLessonsLearned.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
