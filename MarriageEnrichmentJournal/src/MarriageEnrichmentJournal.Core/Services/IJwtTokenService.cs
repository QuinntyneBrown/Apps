using MarriageEnrichmentJournal.Core.Model.UserAggregate;
using MarriageEnrichmentJournal.Core.Model.UserAggregate.Entities;

namespace MarriageEnrichmentJournal.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
