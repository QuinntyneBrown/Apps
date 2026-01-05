using JobSearchOrganizer.Core.Models.UserAggregate;
using JobSearchOrganizer.Core.Models.UserAggregate.Entities;

namespace JobSearchOrganizer.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
