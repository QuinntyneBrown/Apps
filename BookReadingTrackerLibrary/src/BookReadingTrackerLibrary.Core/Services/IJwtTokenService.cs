using BookReadingTrackerLibrary.Core.Model.UserAggregate;
using BookReadingTrackerLibrary.Core.Model.UserAggregate.Entities;

namespace BookReadingTrackerLibrary.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
