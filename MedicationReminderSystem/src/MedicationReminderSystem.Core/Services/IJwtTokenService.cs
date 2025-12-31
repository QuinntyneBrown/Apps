using MedicationReminderSystem.Core.Model.UserAggregate;
using MedicationReminderSystem.Core.Model.UserAggregate.Entities;

namespace MedicationReminderSystem.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
