using FamilyPhotoAlbumOrganizer.Core.Models.UserAggregate;
using FamilyPhotoAlbumOrganizer.Core.Models.UserAggregate.Entities;

namespace FamilyPhotoAlbumOrganizer.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
