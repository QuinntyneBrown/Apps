using FamilyPhotoAlbumOrganizer.Core.Model.UserAggregate;
using FamilyPhotoAlbumOrganizer.Core.Model.UserAggregate.Entities;

namespace FamilyPhotoAlbumOrganizer.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
