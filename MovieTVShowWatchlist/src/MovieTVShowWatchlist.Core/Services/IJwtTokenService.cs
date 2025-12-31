using MovieTVShowWatchlist.Core.Model.UserAggregate;
using MovieTVShowWatchlist.Core.Model.UserAggregate.Entities;

namespace MovieTVShowWatchlist.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
