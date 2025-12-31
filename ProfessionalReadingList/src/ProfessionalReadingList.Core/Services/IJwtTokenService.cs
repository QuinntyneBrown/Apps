using ProfessionalReadingList.Core.Model.UserAggregate;
using ProfessionalReadingList.Core.Model.UserAggregate.Entities;

namespace ProfessionalReadingList.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
