using MovieTVShowWatchlist.Core.Models.UserAggregate;
using MovieTVShowWatchlist.Core.Models.UserAggregate.Entities;

namespace MovieTVShowWatchlist.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
