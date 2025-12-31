using MusicCollectionOrganizer.Core.Model.UserAggregate;
using MusicCollectionOrganizer.Core.Model.UserAggregate.Entities;

namespace MusicCollectionOrganizer.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
