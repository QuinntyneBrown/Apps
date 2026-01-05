using MeetingNotesActionItemTracker.Core.Models.UserAggregate;
using MeetingNotesActionItemTracker.Core.Models.UserAggregate.Entities;

namespace MeetingNotesActionItemTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
