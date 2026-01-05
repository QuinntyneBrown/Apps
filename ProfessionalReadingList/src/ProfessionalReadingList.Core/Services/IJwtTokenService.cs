using ProfessionalReadingList.Core.Models.UserAggregate;
using ProfessionalReadingList.Core.Models.UserAggregate.Entities;

namespace ProfessionalReadingList.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
