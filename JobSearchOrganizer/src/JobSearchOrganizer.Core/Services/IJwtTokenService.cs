using JobSearchOrganizer.Core.Model.UserAggregate;
using JobSearchOrganizer.Core.Model.UserAggregate.Entities;

namespace JobSearchOrganizer.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
