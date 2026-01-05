using BookReadingTrackerLibrary.Core.Models.UserAggregate;
using BookReadingTrackerLibrary.Core.Models.UserAggregate.Entities;

namespace BookReadingTrackerLibrary.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
