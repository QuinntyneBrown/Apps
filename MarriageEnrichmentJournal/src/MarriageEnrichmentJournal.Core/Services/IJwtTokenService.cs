using MarriageEnrichmentJournal.Core.Models.UserAggregate;
using MarriageEnrichmentJournal.Core.Models.UserAggregate.Entities;

namespace MarriageEnrichmentJournal.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
